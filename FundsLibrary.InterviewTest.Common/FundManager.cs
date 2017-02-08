//using RestSharp;
using System;
using System.Collections.Generic;


namespace FundsLibrary.InterviewTest.Common
{
    //made a class ManagedFund to keep the funds organised and be able to use more than just the fund name in the web app.
    public class ManagedFund
    {
        public string FundName { get; }
        public string BaseCurrency { get; }

        public ManagedFund(string FundName, string BaseCurrency)
        {
            this.FundName = FundName;
            this.BaseCurrency = BaseCurrency;
        }
    }

    public class FundManager
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ManagedSince { get; set; }
        public string Biography { get; set; }
        public Location Location { get; set; }

        //method to access the API and return a list of all managed funds
        public List<ManagedFund> GetManagedSecurities()
        {
            
            //      set up a list to take the funds managed by this manager
            List<ManagedFund> ManagersFunds = new List<ManagedFund>();

            //      Commented out API access section as unable to correctly parse the json.
            //      Also tried to used an OData connected service without success.

            //var client = new RestClient();
            //client.BaseUrl = new Uri("https://www.fundslibrary.co.uk/FundsLibrary.DataApi.WebApi");
            //client.AddDefaultHeader("Authorization", "Bearer 0RLR8626VG3I7VXGU1W0");
            //client.AddDefaultHeader("Accept", "application/json");
            //client.AddDefaultHeader("Host", "www.fundslibrary.co.uk");

            //var request = new RestRequest(Method.GET);
            //request.Resource = "Securities?$top=0&$count=true";
            //IRestResponse response = client.Execute(request);

            //      can't parse the json correctly to get to the answers I need.
            //      would need to access the count element returned by API
            //int AllSecuritiesCount = parseJson(response.Content).Count


            //      would use the count returned from that above request
            //      to then feed back in to a request with $top = CountOfAllSecurities
            //var request = new RestRequest(Method.GET);
            //request.Resource = "Securities?$top=" + AllSecuritiesCount.ToString() + "&$count=true";
            //IRestResponse response = client.Execute(request);

            //ParsedJsonObject = parseJson(response.Content)
            
            //      ItemsClass ListOfAllSecurities = ParsedJsonObject.Items
            //      then i'd have a list of every security, I would then loop over every security to check if it was managed by this manager.
            //foreach (SecurityClass Security in ListOfAllSecurities)
            //{
                //TeamClass team = Security.StaticData.Management.Team;
                
                //loop over the team members to find ones with a matching ID to the this.Id
                //if it matches add to list
                //foreach (TeamMemberClass TeamMember in team)
                //{
                    //if (TeamMember.Id == this.Id)
                        //ManagersFunds.add(new ManagedFund(Security.StaticData.Identification.FullName, Security.StaticData.Identification.BaseCurrency));
                //}
            //}


            //Here are a few dummy entries to display in the web app:
            ManagersFunds.Add(new ManagedFund("First Fund", "GBP"));
            ManagersFunds.Add(new ManagedFund("second fund", "USD"));
            ManagersFunds.Add(new ManagedFund("Third fund", "JPY"));

            return ManagersFunds;
        }

    }
}
