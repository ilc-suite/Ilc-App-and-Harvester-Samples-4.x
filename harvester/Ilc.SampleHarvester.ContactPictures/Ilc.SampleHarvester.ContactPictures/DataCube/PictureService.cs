using Ilc.BusinessObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ilc.SampleHarvester.ContactPictures.DataCube
{
    /// <summary>
    /// The PictureService randomly returning a picture url.
    /// It requires a Authentication in order to return proper picture urls 
    /// otherwise it always returns an placeholder picture
    /// </summary>
    public class PictureService
    {
        static List<string> pictureStore = new List<string>() {
            "http://ilc-suite.de/data/uploads/ansprechpartner/B_805f9f9cb0c65736bde574f5e0fdcf9a.png",
            "http://ilc-suite.de/data/uploads/ansprechpartner/B_832a3cf67abd066d42f3da360c836792.png",
            "http://ilc-suite.de/data/uploads/ansprechpartner/B_40a486d94dda4b1037f2df9c28213cb3.png",
            "http://ilc-suite.de/data/uploads/ansprechpartner/B_5faaef646b3a3b782a5c4fb60afbd0f1.png",
            "http://ilc-suite.de/data/uploads/ansprechpartner/B_97e97827a20d375ff85846ab3ef5911b.png",
            "http://ilc-suite.de/data/uploads/ansprechpartner/B_89098299673a6bd7e3c1148698af53ac.png",
            "http://ilc-suite.de/data/uploads/ansprechpartner/B_493e804d1d7e4dc91263ad558aff456b.png",
            "http://ilc-suite.de/data/uploads/ansprechpartner/B_d6031a8301a2b76f7f39d9a348dce3b1.png",
            "http://ilc-suite.de/data/uploads/ansprechpartner/B_840c65ff949a98724133e7a5b7c33d1a.png",
            "http://ilc-suite.de/data/uploads/ansprechpartner/B_4e40ddd82e995c452f04db896c94e106.png",
        };

        static string unAuthenticated = "http://www.ilc-suite.de/data/img/logo.png";

        private Random rnd;
        private bool authenticated = false;

        public PictureService()
        {
            rnd = new Random(DateTime.Now.Second);
        }

        /// <summary>
        /// Authentication is only for showcase. Don't do it like this in productive code!
        /// Normally the authentication would happen in an external system.
        /// </summary>
        public bool Authenticate(string username, string password)
        {
            if (username.Equals("admin", StringComparison.InvariantCultureIgnoreCase) &&
                password.Equals("admin", StringComparison.InvariantCultureIgnoreCase))
            {
                authenticated = true;
            }
            else if (username.Equals("user", StringComparison.InvariantCultureIgnoreCase) &&
                password.Equals("user", StringComparison.InvariantCultureIgnoreCase))
            {
                authenticated = true;
            }
            else
                authenticated = false;                    
            return authenticated;
        }
        
        public string Get(Person person)
        {
            if (!authenticated)
                return unAuthenticated;
            
            var idx = rnd.Next(0, 9);
            return pictureStore[idx];
        }
    }
}