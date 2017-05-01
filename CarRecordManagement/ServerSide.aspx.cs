using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lib.ClassDefinition;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;
using System.Web.Services;
using System.Collections;

namespace sunovaSortingFiltering
{
    public partial class ServerSide : System.Web.UI.Page
    {
        public static List<Car> CarCollection;

        /// <summary>
        /// This PageLoad Method will be called when the application is loaded
        /// At the fist call, it will load the values from URL and save it in CarCollection
        /// All Sesson values will be loaded, Based on the session values grid will be sorted Ascending or descending
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //Fetch the values from URL and add those values to the List
                    CarCollection = LoadValuesFromURL();
                    Session["name"] = "ASC";
                    Session["mileage"] = "ASC";
                    Session["model"] = "ASC";
                    Session["engine"] = "ASC";
                    Session["color"] = "ASC";
                }

                //if car collection is NULL, Load the Values from the URL and display it in Grid
                if (CarCollection != null)
                {
                    serverSide_GridView.DataSource = CarCollection.OrderBy(o => o.mileage).ToList();
                    serverSide_GridView.DataBind();
                }
                else
                {
                    Session["name"] = "ASC";
                    Session["mileage"] = "ASC";
                    Session["model"] = "ASC";
                    Session["engine"] = "ASC";
                    Session["color"] = "ASC";
                    serverSide_GridView.DataSource = LoadValuesFromURL();
                    serverSide_GridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                serverSide_GridView.DataSource = "";
                serverSide_GridView.DataBind();
            }
        }

        /// <summary>
        /// This Method is used to load values from URL and save it in the List and return it
        /// </summary>
        /// <returns></returns>
        public List<Car> LoadValuesFromURL()
        {
            List<Car> listOfCars = null;
            try
            {
                //Fetch the URL from Web.config
               string xmlURL = ConfigurationManager.AppSettings["sunovaUrlInput"].ToString();
               // string xmlURL = System.IO.File.ReadAllText(@"carXML.xml");

                //Idisposable
                using (var webClient = new WebClient())
                {
                    var downloadedValue = webClient.DownloadString(xmlURL);
                    //Installed the JsonConvert from Nuget Package
                    //It will convert the XML to JSON->Deserialize and then List<car>
                    //http://stackoverflow.com/questions/12037085/how-to-convert-xml-to-json-using-c-linq
                    listOfCars = JsonConvert.DeserializeObject<List<Car>>(downloadedValue);
                }
            }
            catch (Exception ex)
            {
                //Add Exception, to be redirect to Error Page
            }
            return listOfCars;
        }

        /// <summary>
        /// This method is caleed from grid and used to bind the values into the Grid and
        /// in some case if the XML is updated, get the updated value into the grid and filter it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LoadGridView(object sender, EventArgs e)
        {
            if (CarCollection != null)
            {
                serverSide_GridView.DataSource = CarCollection.OrderBy(o => o.mileage).ToList();
                serverSide_GridView.DataBind();
            }
            else
            {
                Session["name"] = "ASC";
                Session["mileage"] = "ASC";
                Session["model"] = "ASC";
                Session["engine"] = "ASC";
                Session["color"] = "ASC";
                serverSide_GridView.DataSource = LoadValuesFromURL();
                serverSide_GridView.DataBind();
            }
        }

        /// <summary>
        /// This method is used to sort the table records based on the selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tableSorting(object sender, GridViewSortEventArgs e)
        {
            if (CarCollection == null)
                CarCollection = LoadValuesFromURL();

            switch (e.SortExpression)
            {
                case "mileage":
                    {
                        if (Session["mileage"].Equals("ASC"))
                        {
                            serverSide_GridView.DataSource = CarCollection.OrderByDescending(o => o.mileage);
                            Session["mileage"] = "DESC";
                        }
                        else
                        {
                            serverSide_GridView.DataSource = CarCollection.OrderBy(o => o.mileage);
                            Session["mileage"] = "ASC";
                        }
                        break;
                    }


                case "name":
                    {
                        if (Session["name"].Equals("ASC"))
                        {
                            serverSide_GridView.DataSource = CarCollection.OrderByDescending(o => o.name);
                            Session["name"] = "DESC";
                        }
                        else
                        {
                            serverSide_GridView.DataSource = CarCollection.OrderBy(o => o.name);
                            Session["name"] = "ASC";
                        }
                        break;
                    }
                case "model":
                    {
                        if (Session["model"].Equals("ASC"))
                        {
                            serverSide_GridView.DataSource = CarCollection.OrderByDescending(o => o.model);
                            Session["model"] = "DESC";
                        }
                        else
                        {
                            serverSide_GridView.DataSource = CarCollection.OrderBy(o => o.model);
                            Session["model"] = "ASC";
                        }
                        break;
                    }
                case "engine":
                    {
                        if (Session["engine"].Equals("ASC"))
                        {
                            List<Car> sortedList = new List<Car>();
                            string[] engineSort = CarCollection.Select(c => c.engine.Trim()).Distinct().ToArray();
                            Array.Sort(engineSort, new EngineColumnSortAsc());
                            foreach (string esort in engineSort)
                            {

                                var queryCars = from carList in CarCollection
                                                where carList.engine == esort
                                                select carList;
                                sortedList.AddRange(queryCars);
                            }
                            serverSide_GridView.DataSource = sortedList;
                            Session["engine"] = "DESC";
                        }
                        else
                        {
                            List<Car> sortedList = new List<Car>();
                            string[] engineSort = CarCollection.Select(c => c.engine.Trim()).Distinct().ToArray();
                            Array.Sort(engineSort, new EngineColumnSortDesc());
                            foreach (string esort in engineSort)
                            {

                                var queryCars = from carList in CarCollection
                                                where carList.engine == esort
                                                select carList;
                                sortedList.AddRange(queryCars);
                            }
                            serverSide_GridView.DataSource = sortedList;
                            Session["engine"] = "ASC";

                        }
                        break;
                    }
                default:
                    {
                        if (Session["color"].Equals("ASC"))
                        {
                            serverSide_GridView.DataSource = CarCollection.OrderByDescending(o => o.color);
                            Session["color"] = "DESC";
                        }
                        else
                        {
                            serverSide_GridView.DataSource = CarCollection.OrderBy(o => o.color);
                            Session["color"] = "ASC";
                        }
                        break;
                    }
            }
            serverSide_GridView.DataBind();
        }

        /// <summary>
        /// This method is used to filter the record based on the input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void filter_Click(object sender, EventArgs e)
        {
            try
            {
 
                //Used to get the update values into the grid.
                if (CarCollection == null)
                    LoadGridView(sender, e);

                if (filter_DropDown.SelectedItem.Text.Equals("mileage"))
                {
                    serverSide_GridView.DataSource = (from i in CarCollection
                                                      where i.mileage == Convert.ToDouble(filterValue_TextBox.Text)
                                                      select i);
                }
                else if (filter_DropDown.SelectedItem.Text.Equals("color"))
                {
                    serverSide_GridView.DataSource = (from i in CarCollection
                                                      where i.color == filterValue_TextBox.Text
                                                      select i);
                }
                else if (filter_DropDown.SelectedItem.Text.Equals("model"))
                {
                    serverSide_GridView.DataSource = (from i in CarCollection
                                                      where i.model == filterValue_TextBox.Text
                                                      select i);
                }
                else if (filter_DropDown.SelectedItem.Text.Equals("name"))
                {
                    serverSide_GridView.DataSource = (from i in CarCollection
                                                      where i.name == filterValue_TextBox.Text
                                                      select i);
                }
                else
                {
                    serverSide_GridView.DataSource = (from i in CarCollection
                                                      where i.engine == filterValue_TextBox.Text
                                                      select i);
                }
                serverSide_GridView.DataBind();
            }
            catch (Exception)
            {
                //This exception will be thrown when the LINQ Value is Empty
                serverSide_GridView.DataSource = "";
                serverSide_GridView.DataBind();
            }
        }
    }

    public class EngineColumnSortDesc : IComparer
    {
        List<string> GetList(string s1)
        {
            List<string> SB1 = new List<string>();
            string st1;
            st1 = "";
            bool flag = char.IsDigit(s1[0]);
            foreach (char c in s1)
            {
                if (flag != char.IsDigit(c) || c == '\'')
                {
                    if (st1 != "")
                        SB1.Add(st1);
                    st1 = "";
                    flag = char.IsDigit(c);
                }
                if (char.IsDigit(c))
                {
                    st1 += c;
                }
                if (char.IsLetter(c))
                {
                    st1 += c;
                }


            }
            SB1.Add(st1);
            return SB1;
        }



        public int Compare(object x, object y)
        {
            string s1 = x as string;
            if (s1 == null)
            {
                return 0;
            }
            string s2 = y as string;
            if (s2 == null)
            {
                return 0;
            }
            if (s1 == s2)
            {
                return 0;
            }
            int len1 = s1.Length;
            int len2 = s2.Length;



            // Walk through two the strings with two markers.
            List<string> str1 = GetList(s1);
            List<string> str2 = GetList(s2);
            while (str1.Count != str2.Count)
            {
                if (str1.Count < str2.Count)
                {
                    str1.Add("");
                }
                else
                {
                    str2.Add("");
                }
            }
            int x1 = 0; int res = 0; int x2 = 0; string y2 = "";
            bool status = false;
            string y1 = ""; bool s1Status = false; bool s2Status = false;
            //s1status ==false then string ele int;
            //s2status ==false then string ele int;
            int result = 0;
            for (int i = 0; i < str1.Count && i < str2.Count; i++)
            {
                status = int.TryParse(str1[i].ToString(), out res);
                if (res == 0)
                {
                    y1 = str1[i].ToString();
                    s1Status = false;
                }
                else
                {
                    x1 = Convert.ToInt32(str1[i].ToString());
                    s1Status = true;
                }

                status = int.TryParse(str2[i].ToString(), out res);
                if (res == 0)
                {
                    y2 = str2[i].ToString();
                    s2Status = false;
                }
                else
                {
                    x2 = Convert.ToInt32(str2[i].ToString());
                    s2Status = true;
                }
                //checking --the data comparision
                if (!s2Status && !s1Status)    //both are strings
                {
                    result = str2[i].CompareTo(str1[i]);
                }
                else if (s2Status && s1Status) //both are intergers
                {
                    if (x1 == x2)
                    {
                        if (str2[i].ToString().Length < str1[i].ToString().Length)
                        {
                            result = 1;
                        }
                        else if (str2[i].ToString().Length > str1[i].ToString().Length)
                            result = -1;
                        else
                            result = 0;
                    }
                    else
                    {
                        int st1ZeroCount = str1[i].ToString().Trim().Length - str1[i].ToString().TrimStart(new char[] { '0' }).Length;
                        int st2ZeroCount = str2[i].ToString().Trim().Length - str2[i].ToString().TrimStart(new char[] { '0' }).Length;
                        if (st2ZeroCount > st1ZeroCount)
                            result = -1;
                        else if (st2ZeroCount < st1ZeroCount)
                            result = 1;
                        else
                            result = x2.CompareTo(x1);

                    }
                }
                else
                {
                    result = str2[i].CompareTo(str1[i]);
                }
                if (result == 0)
                {
                    continue;
                }
                else
                    break;

            }
            return result;
        }
    }

    public class EngineColumnSortAsc : IComparer
    {
        List<string> GetList(string s1)
        {
            List<string> SB1 = new List<string>();
            string st1;
            st1 = "";
            bool flag = char.IsDigit(s1[0]);
            foreach (char c in s1)
            {
                if (flag != char.IsDigit(c) || c == '\'')
                {
                    if (st1 != "")
                        SB1.Add(st1);
                    st1 = "";
                    flag = char.IsDigit(c);
                }
                if (char.IsDigit(c))
                {
                    st1 += c;
                }
                if (char.IsLetter(c))
                {
                    st1 += c;
                }


            }
            SB1.Add(st1);
            return SB1;
        }



        public int Compare(object x, object y)
        {
            string s1 = x as string;
            if (s1 == null)
            {
                return 0;
            }
            string s2 = y as string;
            if (s2 == null)
            {
                return 0;
            }
            if (s1 == s2)
            {
                return 0;
            }
            int len1 = s1.Length;
            int len2 = s2.Length;



            // Walk through two the strings with two markers.
            List<string> str1 = GetList(s1);
            List<string> str2 = GetList(s2);
            while (str1.Count != str2.Count)
            {
                if (str1.Count < str2.Count)
                {
                    str1.Add("");
                }
                else
                {
                    str2.Add("");
                }
            }
            int x1 = 0; int res = 0; int x2 = 0; string y2 = "";
            bool status = false;
            string y1 = ""; bool s1Status = false; bool s2Status = false;
            //s1status ==false then string ele int;
            //s2status ==false then string ele int;
            int result = 0;
            for (int i = 0; i < str1.Count && i < str2.Count; i++)
            {
                status = int.TryParse(str1[i].ToString(), out res);
                if (res == 0)
                {
                    y1 = str1[i].ToString();
                    s1Status = false;
                }
                else
                {
                    x1 = Convert.ToInt32(str1[i].ToString());
                    s1Status = true;
                }

                status = int.TryParse(str2[i].ToString(), out res);
                if (res == 0)
                {
                    y2 = str2[i].ToString();
                    s2Status = false;
                }
                else
                {
                    x2 = Convert.ToInt32(str2[i].ToString());
                    s2Status = true;
                }
                //checking --the data comparision
                if (!s2Status && !s1Status)    //both are strings
                {
                    result = str1[i].CompareTo(str2[i]);
                }
                else if (s2Status && s1Status) //both are intergers
                {
                    if (x1 == x2)
                    {
                        if (str1[i].ToString().Length < str2[i].ToString().Length)
                        {
                            result = 1;
                        }
                        else if (str1[i].ToString().Length > str2[i].ToString().Length)
                            result = -1;
                        else
                            result = 0;
                    }
                    else
                    {
                        int st1ZeroCount = str1[i].ToString().Trim().Length - str1[i].ToString().TrimStart(new char[] { '0' }).Length;
                        int st2ZeroCount = str2[i].ToString().Trim().Length - str2[i].ToString().TrimStart(new char[] { '0' }).Length;
                        if (st2ZeroCount > st1ZeroCount)
                            result = -1;
                        else if (st1ZeroCount < st2ZeroCount)
                            result = 1;
                        else
                            result = x1.CompareTo(x2);

                    }
                }
                else
                {
                    result = str1[i].CompareTo(str2[i]);
                }
                if (result == 0)
                {
                    continue;
                }
                else
                    break;

            }
            return result;
        }
    }
}