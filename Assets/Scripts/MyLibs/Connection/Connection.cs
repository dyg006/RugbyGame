using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DataBase{



	public static class Connection  {
		public static string BASE_URL= "";
		//Called when there was a problem in the execution of the function
		public delegate void errorFunction ();
		//Called when the user logged correctly after using loginCoachdata
		public delegate void loggedFunction (float mediumCalifications);
		//Called when the ranking is loaded
		public delegate void getTable (RankingEntry[] table);

        public delegate void getTable2(VideogameStatistics[] table);

        public delegate void getTable3(VideogameValues[] table);

        public delegate void successFunction();
		//Change the value in this file if the URL of the server where the game conects changes
		private const string MASTER_FILE = "MasterURL.conf";
		private const string GAME_LOGIN = "/coachdata/gamelogin";
		//private const string MATCH_RESULTS = "/coachdata/gamegetranking";
		private const string SEND_RESULTS = "/coachdata/gamesaveresult";
        private const string GET_STATISTICS = "/coachdata/rugbyrungetstatistics";
        private const string GET_VALUES = "/coachdata/rugbyrungetvalues";
        private const string SAVE_STATISTICS = "/coachdata/rugbyrunsavestatistics";
        private const string SAVE_VALUES = "/coachdata/rugbyrunsavevalues";
        private const string GET_ALL_STATISTICS = "/coachdata/rugbyrungetallstatistics";

        private static WWW masterWebRequest;
        public class RankingEntry{
			public string user;
			public int won;
			public int drawn;
			public int lost;         
			public int goalsFor;     //Niveles que te has pasado
			public int goalsAgainst; //Niveles totales
			public int points;
		}

        public class VideogameStatistics{
            public string user;
            public int levelReached;
            public float yards;
            public int acquiredSpeed;
            public int acquiredJumps;
            public int acquiredLifes;
            public float timePlayedInSeconds;
            public float timePlayedInMinutes;
        }

        public class VideogameValues{
            public string user;
            public int level;
            public int points;
            public int velocity;
            public int numJumps;
            public int lifes;
        }



        /*
		 *Sends the user to log in to coachdata app and receives data from
		 *the server
		 *Function redone from the conection.js of Mateo
		 *
		 *This function is a coroutine and must be used with startCoroutine instead of called directly
		 *
		 *@param user: the user string
		 *@param loggedSuccesfully: function called if the user manages to log in (can be null)
		 *@param error: function called if the user fails to log in (can be null)
		 */
        public static IEnumerator loginCoachData(string user, loggedFunction loggedSuccesfully, errorFunction error)
        {
            float evaluation = 0;
            WWW request;
            Dictionary<string, string> args = new Dictionary<string, string>();
            IEnumerator auxRet;
            do
            {
                auxRet = setBaseUrl();
                if (auxRet != null)
                    yield return auxRet;
            } while (auxRet != null);

            args.Add("user", user);
            request = new WWW(BASE_URL + GAME_LOGIN + GetParameters(args));
            while (!request.isDone)
                yield return request;
            /*Debug.LogError ("REQUEST:"+BASE_URL + GAME_LOGIN+GetParameters(args));
			Debug.LogError ("TEXT:"+request.text);*/
            if (request.error == null)
            {

                loginCoachDataResponse aux = JsonConvert.DeserializeObject<loginCoachDataResponse>(request.text);
                if (aux.response == "OK")
                {

                    if (aux.evaluated)
                    {
                        foreach (KeyValuePair<string, JToken> pair in aux.evaluations)
                        {
                            evaluation += pair.Value.ToObject<int>();
                        }
                        evaluation /= (float)(aux.evaluations.Count * aux.max_mark);
                    }
                    if (loggedSuccesfully != null)
                        loggedSuccesfully(evaluation);

                }
                else
                {
                    if (error != null)
                        error();
                }
            }
            else if (error != null)
                error();
        }


        /******************************************************************************************************************/
        public static IEnumerator sendVideoGameStatistics(string user, int levelReached, float yards, int acquiredSpeed, 
            int acquiredJumps, int acquiredLifes, float timePlayedInSeconds, successFunction success, errorFunction error)
        {
            WWW request;
            Dictionary<string, string> args = new Dictionary<string, string>();
            IEnumerator auxRet;
            do
            {
                auxRet = setBaseUrl();
                if (auxRet != null)
                    yield return auxRet;
            } while (auxRet != null);
            args.Add("user", user);
            args.Add("levelReached", levelReached.ToString());
            args.Add("yards", yards.ToString());
            args.Add("acquiredSpeed", acquiredSpeed.ToString());
            args.Add("acquiredJumps", acquiredJumps.ToString());
            args.Add("acquiredLifes", acquiredLifes.ToString());
            args.Add("timePlayedInSeconds", timePlayedInSeconds.ToString());
            args.Add("timePlayedInMinutes", (timePlayedInSeconds/60).ToString());
            request = new WWW(BASE_URL + SAVE_STATISTICS + GetParameters(args));
            yield return request;
            if (request.error == null)
            {
                loginCoachDataResponse aux = JsonConvert.DeserializeObject<loginCoachDataResponse>(request.text);
                if (aux.response != "OK")
                {
                    if (error != null)
                        error();
                }
                else
                {
                    if (success != null)
                        success();
                }
            }
            else
                if (error != null)
                error();

        }

        /******************************************************************************************************************/
        public static IEnumerator sendVideoGameValues(string user, int level, int points, int velocity, int numJumps, int lifes,
            successFunction success, errorFunction error)
        {
            WWW request;
            Dictionary<string, string> args = new Dictionary<string, string>();
            IEnumerator auxRet;
            do
            {
                auxRet = setBaseUrl();
                if (auxRet != null)
                    yield return auxRet;
            } while (auxRet != null);
            args.Add("user", user);
            args.Add("level", level.ToString());
            args.Add("points", points.ToString());
            args.Add("velocity", velocity.ToString());
            args.Add("numJumps", numJumps.ToString());
            args.Add("lifes", lifes.ToString());
            request = new WWW(BASE_URL + SAVE_VALUES + GetParameters(args));
            yield return request;
            if (request.error == null)
            {
                loginCoachDataResponse aux = JsonConvert.DeserializeObject<loginCoachDataResponse>(request.text);
                if (aux.response != "OK")
                {
                    if (error != null)
                        error();
                }
                else
                {
                    if (success != null)
                        success();
                }
            }
            else
                if (error != null)
                error();

        }


        /********************************************************************************************************/
        /********************************************************************************************************/
        /********************************************************************************************************/
        /********************************************************************************************************/
        public static IEnumerator getVideoGameStatistics(string user, getTable2 gettable, errorFunction error)
        {
            WWW request;
            VideogameStatistics[] videoStat;
            Dictionary<string, string> args = new Dictionary<string, string>();
            IEnumerator auxRet;
            do
            {
                auxRet = setBaseUrl();
                if (auxRet != null)
                    yield return auxRet;
            } while (auxRet != null);
            args.Add("user", user);
            request = new WWW(BASE_URL + GET_STATISTICS + GetParameters(args));
            Debug.Log(BASE_URL + GET_STATISTICS + GetParameters(args));
            yield return request;
            if (request.error == null)
            {
                getStatistics aux = JsonConvert.DeserializeObject<getStatistics>(request.text);
                if (aux.response == "OK")
                {
                    VideogameStatistics vs;
                    videoStat = new VideogameStatistics[100];
                    
                    vs = new VideogameStatistics();
                    vs.user = aux.user;
                    vs.levelReached = aux.levelReached;
                    vs.yards = aux.yards;
                    vs.acquiredSpeed = aux.acquiredSpeed;
                    vs.acquiredJumps = aux.acquiredJumps;
                    vs.acquiredLifes = aux.acquiredLifes;
                    vs.timePlayedInSeconds = aux.timePlayedInSeconds;
                    vs.timePlayedInMinutes = aux.timePlayedInMinutes;
                    videoStat[0] = vs;
                    
                    if (gettable != null)
                        gettable(videoStat);
                }
                else
                {
                    if (error != null)
                        error();
                }
            }
            else
            {
                if (error != null)
                    error();
            }
        }


        public static IEnumerator getAllVideogameStatistics(string user, getTable2 gettable, errorFunction error)
        {
            WWW request;
            VideogameStatistics[] statisticsTable;
            setBaseUrl();
            request = new WWW(BASE_URL + GET_ALL_STATISTICS);
            Debug.Log(BASE_URL + GET_ALL_STATISTICS);
            yield return request;
            if (request.error == null)
            {
                getAllStatistics aux = JsonConvert.DeserializeObject<getAllStatistics>(request.text);
                if (aux.response == "OK")
                {
                    VideogameStatistics vs;
                    statisticsTable = new VideogameStatistics[aux.user.Count];
                    JToken auxToken;
                    string number;
                    for (int i = 0; i < aux.user.Count; i++)
                    {
                        vs = new VideogameStatistics();
                        number = i.ToString();

                        aux.user.TryGetValue(number, out auxToken);
                        vs.user = auxToken.ToObject<string>();
                        aux.levelReached.TryGetValue(number, out auxToken);
                        vs.levelReached = auxToken.ToObject<int>();
                        aux.yards.TryGetValue(number, out auxToken);
                        vs.yards = auxToken.ToObject<float>();
                        aux.acquiredSpeed.TryGetValue(number, out auxToken);
                        vs.acquiredSpeed = auxToken.ToObject<int>();
                        aux.acquiredJumps.TryGetValue(number, out auxToken);
                        vs.acquiredJumps = auxToken.ToObject<int>();
                        aux.acquiredLifes.TryGetValue(number, out auxToken);
                        vs.acquiredLifes = auxToken.ToObject<int>();
                        aux.timePlayedInSeconds.TryGetValue(number, out auxToken);
                        vs.timePlayedInSeconds = auxToken.ToObject<float>();
                        aux.timePlayedInMinutes.TryGetValue(number, out auxToken);
                        vs.timePlayedInMinutes = auxToken.ToObject<float>();
                        statisticsTable[i] = vs;
                    }
                    if (gettable != null)
                        gettable(statisticsTable);
                }
                else
                {
                    if (error != null)
                        error();
                }
            }
            else
                if (error != null)
                error();
        }



        /********************************************************************************************************/
        /********************************************************************************************************/
        /********************************************************************************************************/
        /********************************************************************************************************/
        public static IEnumerator getVideoGameValues(string user, getTable3 gettable, errorFunction error)
        {
            WWW request;
            VideogameValues[] videoVal;
            Dictionary<string, string> args = new Dictionary<string, string>();
            IEnumerator auxRet;
            do
            {
                auxRet = setBaseUrl();
                if (auxRet != null)
                    yield return auxRet;
            } while (auxRet != null);
            args.Add("user", user);
            request = new WWW(BASE_URL + GET_VALUES + GetParameters(args));
            Debug.Log(BASE_URL + GET_VALUES + GetParameters(args));
            yield return request;
            if (request.error == null)
            {
                getVideogameValues aux = JsonConvert.DeserializeObject<getVideogameValues>(request.text);
                if (aux.response == "OK")
                {
                    VideogameValues vvs;
                    videoVal = new VideogameValues[100];
                    vvs = new VideogameValues();
                   

                    vvs.user = aux.user;
                    vvs.level = aux.level;
                    vvs.points = aux.points;
                    vvs.velocity = aux.velocity;
                    vvs.numJumps = aux.numJumps;
                    vvs.lifes = aux.lifes; 
                    videoVal[0] = vvs;
                    
                    if (gettable != null) { 
                        gettable(videoVal);
                    }
                }
                else
                {
                    if (error != null)
                        error();
                }
            }
            else
                if (error != null)
                error();
        }


        /**Returns the header as a string ready to add to a get html request
		 * @Params headers a hastable with the pairs key value as a strings to send
		 * 
		 * @return a string ready to add to an url in a get request
		 */
        public static string GetParameters(Dictionary<string,string> headers){
			string ret;
			bool notFirst = false;
			ret = "/?";
			if (headers.Count == 0) {
				return "";
			}
			foreach (string key in headers.Keys) {
				if(notFirst)
					ret+= "&"+WWW.EscapeURL(key) +"=" + WWW.EscapeURL(headers[key]);
				else{
					ret+= WWW.EscapeURL(key) +"=" + WWW.EscapeURL(headers[key]);
					notFirst = true;
				}
			}
			return ret;

		}
        private static IEnumerator setBaseUrl()
        {

            /*Debug.LogError ("BaseURL="+BASE_URL);*/
            if (BASE_URL != "")
            {
                return null;
            }
            string filePath = Path.Combine(Application.streamingAssetsPath, MASTER_FILE);
            if (filePath.Contains("://") || filePath.Contains(":///"))
            {
                if (masterWebRequest == null)
                {
                    masterWebRequest = new WWW(filePath);

                }
                if (masterWebRequest.isDone)
                {
                    BASE_URL = masterWebRequest.text.Trim();
                    return null;
                }
                return masterWebRequest;
            }
            StreamReader sr = new StreamReader(filePath);
            BASE_URL = sr.ReadToEnd();
            return null;
        }

        private class loginCoachDataResponse
        {
            public int max_mark;
            public JObject evaluations;
            public string response;
            public bool evaluated;
        }

        /*
		private class getMatchsResultsDataResponse{
			public JObject goalsAgainst;
			public JObject won;
			public JObject user;
			public JObject lost;
			public JObject drawn;
			public JObject points;
			public string response;
			public JObject goalsFor;
		}
        */

        private class getAllStatistics
        {
            public JObject user;
            public JObject levelReached;
            public JObject yards;
            public JObject acquiredSpeed;
            public JObject acquiredJumps;
            public JObject acquiredLifes;
            public JObject timePlayedInSeconds;
            public JObject timePlayedInMinutes;
            public string response;
        }

        private class getStatistics
        {
            public string user;
            public int levelReached;
            public float yards;
            public int acquiredSpeed;
            public int acquiredJumps;
            public int acquiredLifes;
            public float timePlayedInSeconds;
            public float timePlayedInMinutes;
            public string response;
        }

        private class getVideogameValues
        {
            public string user;
            public int level;
            public int points;
            public int velocity;
            public int numJumps;
            public int lifes;
            public string response;
        }
    }
}

