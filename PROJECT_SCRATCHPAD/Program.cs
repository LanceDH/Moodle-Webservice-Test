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
            public void P(string key, string value){
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

        static void Main(string[] args)
        {
            // "localhost/login/token.php"
            // "localhost/webservice/rest/server.php"



            //String localhost = "localhost";
            string localhost = "moodle-cvomobile.rhcloud.com";
            //String localhost = "moodle.cvoantwerpen.be";

            Package px =
                new Package(localhost + "/login/token.php");
                //px.P("username", "admin");
                //px.P("password", "Boerderijm1n#s");
                px.P("username", "admin");
                px.P("password", "Boerderijm1n#s");
                px.P("service", "moodle_mobile_app");
                
                string token = (String)JsonParser.FromJson(px.Execute())["token"];
                Console.WriteLine(token);

            Package user =
                new Package(localhost + "/webservice/rest/server.php");
                user.P("wstoken", token);
                user.P("wsfunction", "core_webservice_get_site_info");
                user.P("moodlewsrestformat", "json");
                
                string userid = Convert.ToString(JsonParser.FromJson(user.Execute())["userid"]);
                Console.WriteLine("User Id: " + userid);

            Package course =
                new Package(localhost + "/webservice/rest/server.php");
                course.P("wstoken", token);
                course.P("wsfunction", "core_enrol_get_users_courses");
                course.P("moodlewsrestformat", "json");
                course.P("userid", userid);
                Console.WriteLine(course.Execute());

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

                foreach (Moodle.BLL.Assignment ass in Model.AssingmentSelectAll(localhost, token, 4))
	            {
                    //CalendarEvent c = new CalendarEvent(assignment);
                    Console.WriteLine(ass.Id + ": " + ass.Name);


                    foreach (Moodle.BLL.Grade grade in Model.GradeSelectAll(localhost, token, ass.Id))
                    {
                        Console.WriteLine(grade.UserId + ": " + grade.Score);
                    }

                    string userGrade = Model.GradeSelectOne(localhost, token, ass.Id, Convert.ToInt32(userid));
                    
                    Console.WriteLine("Your score: " + (!userGrade.Equals("-1")? userGrade : "No score yet"));

                    /*
                    // Asignments per course
                    Package grade =
                    new Package(localhost + "/webservice/rest/server.php");
                    grade.P("wstoken", token);
                    grade.P("wsfunction", "mod_assign_get_grades");
                    grade.P("moodlewsrestformat", "json");
                    grade.P("assignmentids[0]", ass.Id.ToString());

                    JObject grades = new JObject();
                    try
                    {
                        grades = JObject.Parse(grade.Execute());
                        //Console.WriteLine(assignments);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    Console.WriteLine(grades);


                    Console.WriteLine();*/
	            }

            
                

                //// Asignments per course
                //Package assignment =
                //new Package(localhost + "/webservice/rest/server.php");
                //assignment.P("wstoken", token);
                //assignment.P("wsfunction", "mod_assign_get_assignments");
                //assignment.P("moodlewsrestformat", "json");
                //assignment.P("courseids[0]", "4");

                //JObject assignments = new JObject();
                //try
                //{
                //    assignments = JObject.Parse(assignment.Execute());
                //    //Console.WriteLine(assignments);


                //    foreach (JObject c in assignments["courses"])
                //    {
                //        foreach (JObject ass in c["assignments"])
                //        {
                //            //CalendarEvent c = new CalendarEvent(assignment);
                //            Console.WriteLine(ass["id"]);

                //            // Asignments per course
                //            Package grade =
                //            new Package(localhost + "/webservice/rest/server.php");
                //            grade.P("wstoken", token);
                //            grade.P("wsfunction", "mod_assign_get_grades");
                //            grade.P("moodlewsrestformat", "json");
                //            grade.P("assignmentids[0]", ass["id"].ToString());

                //            JObject grades = new JObject();
                //            try
                //            {
                //                grades = JObject.Parse(grade.Execute());
                //                //Console.WriteLine(assignments);
                //            }
                //            catch (Exception e)
                //            {
                //                Console.WriteLine(e.Message);
                //            }

                //            Console.WriteLine(grades);


                //            Console.WriteLine();

                //        }
                //    }


                //   // Console.WriteLine(assignments);
                //    //Console.WriteLine(Convert.ToInt32(assignments["courses"]));
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine(e.Message);
                //}





                

            Console.Read();
        }
    }
}
