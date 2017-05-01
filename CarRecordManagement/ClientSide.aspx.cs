using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lib.ClassDefinition;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using System.Web.Services;

namespace sunovaSortingFiltering
{
    public partial class ClientSide : System.Web.UI.Page
    {
        public static List<Car> CarCollection;
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
                if (CarCollection == null)
                {
                    CarCollection=LoadValuesFromURL();
                }
            }
            catch (Exception ex)
            {

            }
        }


        public List<Car> LoadValuesFromURL()
        {
            List<Car> listOfCars = null;
            try
            {
                //Fetch the URL from Web.config
                string xmlURL = ConfigurationManager.AppSettings["sunovaUrlInput"].ToString();

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

        //Calling WebMethod using Angular JS
        [WebMethod]
        public static List<Car> GetCars()
        {
            return CarCollection;
        }
    }
}