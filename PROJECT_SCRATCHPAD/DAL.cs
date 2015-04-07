﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
//using PROJECT_SCRATCHPAD;
using Newtonsoft.Json.Linq;
using Json;

namespace PROJECT_SCRATCHPAD.DAL
{

    class Package
    {
        string url;
        List<KeyValuePair<string, string>> parameters;

        // Stel Url in
        public Package(String url)
        {
            this.url = url;
            parameters = new List<KeyValuePair<string, string>>();
        }

        // Voeg Input Parameters toe
        public void P(string key, string value)
        {
            parameters.Add(new KeyValuePair<string, string>(key, value));
        }


        // Voer [Web Service Request] uit
        public String Execute()
        {
            string output = "";
            string param_string = "";

            foreach (KeyValuePair<string, string> dx in parameters)
                param_string += dx.Key + "=" + dx.Value + "&";

            byte[] buffer = Encoding.ASCII.GetBytes(param_string);
            string lex = "http://" + url + "?" + param_string;

            //Console.WriteLine(lex);

            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(lex);
            WebReq.Method = WebRequestMethods.Http.Post;
            WebReq.ContentType = "application/x-www-form-urlencoded";

            WebReq.ContentLength = buffer.Length;
            using (Stream PostData = WebReq.GetRequestStream())
                PostData.Write(buffer, 0, buffer.Length);

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
            using (StreamReader reader = new StreamReader(WebResp.GetResponseStream()))
                output = reader.ReadToEnd();

            //Console.WriteLine(output);

            return output;

        }
    }


    public class Assignment
    {
        public static List<PROJECT_SCRATCHPAD.BLL.Assignment> SelectAll(string url, string token, int courseId)
        {
            List<BLL.Assignment> assignments = new List<BLL.Assignment>();


            Package pAssignment =
                new Package(url + "/webservice/rest/server.php");
            pAssignment.P("wstoken", token);
            pAssignment.P("wsfunction", "mod_assign_get_assignments");
            pAssignment.P("moodlewsrestformat", "json");
            pAssignment.P("courseids[0]", "" + courseId);
            JObject jCourses = new JObject();

            try
            {
                jCourses = JObject.Parse(pAssignment.Execute());
                JObject jAssignments = (JObject)jCourses["courses"][0];
                //Console.WriteLine(assignments);


                foreach (JObject c in jAssignments["assignments"])
                {
                    BLL.Assignment a = new BLL.Assignment(c);
                    assignments.Add(a);
                }
                    
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return assignments;
        }

    }
}
