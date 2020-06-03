﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class ExternalFlights : IExternalFlights
    {
        private static Dictionary<string, string> DicFlightServer;
        private readonly ISQLCommands commands;
        public ExternalFlights(ISQLCommands commands)
        {
            this.commands = commands;
        }
        public async Task<FlightPlan> GetExternalFlightById(string id)
        {
            FlightPlan flightPlan = null;
            // List<FlightPlan> ex = new List<FlightPlan>();
            //Get
            //api/FlightPlan/" + id;
           // SQLCommands commands = new SQLCommands();
            //find the server of the flight id
            string serverId = DicFlightServer[id];
            if (serverId == null)
            {
                return null;
            }
            else
            {
                Server server = commands.serverById(serverId);
                //like ther= rest of the code


                //  List<Server> ourServers = commands.ServerList();
                //foreach (Server server in ourServers)
                // {
                string idFlight = id;
                string urlServer = server.ServerUrl;
                string serverApi = urlServer + "/api/FlightPlan/" + idFlight;
                //string strurltest = String.Format("https://jsonplaceholder.typicode.com/posts/1/comments");
                string strurltest = String.Format(serverApi);
                WebRequest requestObjGet = WebRequest.Create(strurltest);
                requestObjGet.Method = "GET";
                WebResponse responseObjGet = null;
                responseObjGet = await requestObjGet.GetResponseAsync();
                string strresulttest = null;
                using (Stream stream = responseObjGet.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    strresulttest = sr.ReadToEnd();
                    sr.Close();
                }
                flightPlan = JsonConvert.DeserializeObject<FlightPlan>(strresulttest);
            }
            //    }

            return flightPlan;
        }
        public async Task<List<Flight>> GetExternalFlights(string time)
        {
            DicFlightServer = new Dictionary<string, string>();
            List<Flight> ex = null;
            // List<FlightPlan> ex = new List<FlightPlan>();
            //Get
            //api/FlightPlan/" + id;
            // SQLCommands commands = new SQLCommands();
            List<Server> ourServers = commands.ServerList();
            foreach (Server server in ourServers)
            {
                string date_time = time;
                string urlServer = server.ServerUrl;
                string serverApi = urlServer + "/api/Flights?relative_to=" + date_time;
                //string strurltest = String.Format("https://jsonplaceholder.typicode.com/posts/1/comments");
                string strurltest = String.Format(serverApi);
                WebRequest requestObjGet = WebRequest.Create(strurltest);
                requestObjGet.Method = "GET";
                WebResponse responseObjGet = null;
                try
                {
                    responseObjGet = await requestObjGet.GetResponseAsync();
                    string strresulttest = null;
                    using (Stream stream = responseObjGet.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(stream);
                        strresulttest = sr.ReadToEnd();
                        sr.Close();
                    }
                  
                     ex = JsonConvert.DeserializeObject<List<Flight>>(strresulttest);
                    string serverId = server.ServerId;
                    this.AddToDic(ex, serverId);
                    /*//add mapping from each id flight to the server id
                    foreach (Flight item in ex)
                    {
                        string flightId = item.Flight_id;
                        DicFlightServer.Add(flightId,serverId );
                  */
                   // }
                }
                catch (Exception)
                {
                    Console.WriteLine("server failed");
                }
            }

            return ex;
        }

        public void AddToDic(List<Flight> listFlight, string serverId)
        {
            int i;
                //add new server and flight id to dic
                for ( i=0;i<listFlight.Count;i++)
                {
                    DicFlightServer.Add(listFlight[i].Flight_id, serverId);
                }
          

        }
        public List<Flight> changeBoolEX(List<Flight> flights)
        {
            foreach (Flight item in flights)
            {
                item.Is_external = true;
            }
            return flights;
        }
        public void DeleteDic(string id)
        {
            //remove all the flights with this id server from the dic
            foreach (var item in DicFlightServer)
            {
                if (item.Value == id)
                {
                    DicFlightServer.Remove(item.Key);
                }
            }
        }
    }
}
