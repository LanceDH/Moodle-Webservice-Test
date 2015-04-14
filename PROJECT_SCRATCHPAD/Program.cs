using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Json;
using Newtonsoft.Json.Linq;
using Moodle;

namespace PROJECT_SCRATCHPAD
{

   

    class Program
    {
        //class Package
        //{
        //    string url;
        //    List<KeyValuePair<string, string>> parameters;

        //    // Stel Url in
        //    public Package(String url)
        //    {
        //        this.url = url;
        //        parameters = new List<KeyValuePair<string, string>>();
        //    }

        //    // Voeg Input Parameters toe
        //    public void P(string key, string value){
        //        parameters.Add(new KeyValuePair<string, string>(key, value));
        //    }


        //    // Voer [Web Service Request] uit
        //    public String Execute()
        //    {
        //        string output = "";
        //        string param_string = "";

        //        foreach (KeyValuePair<string, string> dx in parameters)
        //            param_string += dx.Key + "=" + dx.Value + "&";

        //        byte[] buffer = Encoding.ASCII.GetBytes(param_string);
        //        string lex = "http://" + url + "?" + param_string;

        //        //Console.WriteLine(lex);

        //        HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(lex);
        //        WebReq.Method = WebRequestMethods.Http.Post;
        //        WebReq.ContentType = "application/x-www-form-urlencoded";

        //        WebReq.ContentLength = buffer.Length;
        //        using (Stream PostData = WebReq.GetRequestStream())
        //            PostData.Write(buffer, 0, buffer.Length);

        //        HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
        //        using (StreamReader reader = new StreamReader(WebResp.GetResponseStream()))
        //            output = reader.ReadToEnd();

        //        //Console.WriteLine(output);

        //        return output;
                
        //    }
        //}

        static void Main(string[] args)
        {
            // "localhost/login/token.php"
            // "localhost/webservice/rest/server.php"



            //String localhost = "localhost";
            string localhost = "moodle-cvomobile.rhcloud.com";
            //String localhost = "moodle.cvoantwerpen.be";

            string token = Moodle.Model.RequestTokenForService(localhost, "cvomobile", "Boerderijm1n#s", "mobile");

            //Moodle.DAL.Package px = new Moodle.DAL.Package(localhost + "/login/token.php");
            //    //px.P("username", "admin");
            //    //px.P("password", "Boerderijm1n#s");
            //    px.P("username", "cvomobile");
            //    px.P("password", "Boerderijm1n#s");
            //    px.P("service", "mobile");

            //    string token = (String)JsonParser.FromJson(px.Execute())["token"];

            

                //string token = "203f82380a5ca94b230ed5ee0e1fd061";//
                Console.WriteLine("Token: " + token);

                Console.Write("username: ");
                string userName = Console.ReadLine();

                Moodle.DAL.Package puser = new Moodle.DAL.Package(localhost + "/webservice/rest/server.php");
                puser.P("wstoken", token);
                puser.P("wsfunction", "core_user_get_users_by_field");
                puser.P("moodlewsrestformat", "json");
                puser.P("field", "email");
                puser.P("values[0]", userName);

                //Package puser = new Package(localhost + "/webservice/rest/server.php");
                //puser.P("wstoken", token);
                //puser.P("wsfunction", "core_user_get_users");
                //puser.P("moodlewsrestformat", "json");
                //puser.P("criteria[0][key]", "id");
                //puser.P("criteria[0][value]", "15");
                string userid = "1";//Convert.ToString(JsonParser.FromJson());
                try
                {
                    JArray jUser = JArray.Parse(puser.Execute());
                    userid = (string)jUser[0]["id"];
                    Console.WriteLine("User Id: " + userid);

                    //JObject jUser = JObject.Parse(puser.Execute());
                    ////userid = (string)jUser[0]["id"];
                    //Console.WriteLine("User Id: " + jUser);
                    //
//string userid = "9";//Convert.ToString(JsonParser.FromJson());

                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                


           /* Package user =
                new Package(localhost + "/webservice/rest/server.php");
                user.P("wstoken", token);
                user.P("wsfunction", "core_webservice_get_site_info");
                user.P("moodlewsrestformat", "json");
                
                string userid = Convert.ToString(JsonParser.FromJson(user.Execute())["userid"]);
                Console.WriteLine("User Id: " + userid);*/

                Moodle.DAL.Package course =
                new Moodle.DAL.Package(localhost + "/webservice/rest/server.php");
                course.P("wstoken", token);
                course.P("wsfunction", "core_enrol_get_users_courses");
                course.P("moodlewsrestformat", "json");
                course.P("userid", userid);
                JArray jCourse = JArray.Parse(course.Execute());
                Console.WriteLine("Courses: " + jCourse);

                Console.WriteLine();

            /*Package courseInfo =
                new Package(localhost + "/webservice/rest/server.php");
                course.P("wstoken", token);
                course.P("wsfunction", "core_enrol_get_users_courses");
                course.P("moodlewsrestformat", "json");
                course.P("userid", userid);

                Console.WriteLine(course.Execute());*/

           // // events global
           //Package calender =
           //     new Package(localhost + "/webservice/rest/server.php");
           //     calender.P("wstoken", token);
           //     calender.P("wsfunction", "core_calendar_get_calendar_events");
           //     calender.P("moodlewsrestformat", "json");
           //     calender.P("events[courseids][0]", "2");
           //     //calender.P("options[userevents]", "1");
           //     //calender.P("options[userevents]", "1");
           //     //calender.P("options[siteevents]", "1");

           //     JObject assignments = new JObject();
           //     try
           //     {
           //         assignments = JObject.Parse(calender.Execute());
           //         //Console.WriteLine(assignments);
           //     }
           //     catch (Exception e)
           //     {
           //         Console.WriteLine(e.Message);
           //     }

           //     foreach (JObject assignment in assignments["events"])
           //     {
           //         CalendarEvent c = new CalendarEvent(assignment);
           //         Console.WriteLine(c.ToString());
           //         Console.WriteLine();
           //     }
            //Convert.ToInt32( jCourse[0]["id"])
                foreach (Moodle.BLL.Assignment ass in Model.AssingmentSelectAll(localhost, token, 4))
	            {
                    //CalendarEvent c = new CalendarEvent(assignment);
                    Console.WriteLine(ass.Id + ": " + ass.Name);

                    //all scores
                    foreach (Moodle.BLL.Grade grade in Model.GradeSelectAll(localhost, token, ass.Id))
                    {
                        Console.WriteLine(grade.UserId + ": " + grade.Score);
                    }
                    
                    //only scores of user
                    string userGrade = Model.GradeSelectOne(localhost, token, ass.Id, Convert.ToInt32(userid));
                    
                    Console.WriteLine("Your score: " + (!userGrade.Equals("-1")? userGrade : "No score yet"));
                    Console.WriteLine();
	            }

            Console.Read();
        }
    }
}
