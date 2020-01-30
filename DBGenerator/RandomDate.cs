using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBGenerator
{
    public class RandomDate
    {
        //DateTime startLanding;
        //DateTime startDeparture;
        //Random gen;
        //int range;

        //public RandomDate()
        //{

        //    startDeparture = new DateTime(2019, 8, 1);
        //    gen = new Random();
        //    range = (DateTime.Today - startDeparture).Days;


        //    startLanding = new DateTime(2019, 9, 1);
        //    gen = new Random();
        //    range = (DateTime.Today - startLanding).Days;
        //}

        //public DateTime DepartureDate()
        //{
        //    return startDeparture.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
        //}
        //public DateTime LandingDate()
        //{
        //    return startLanding.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
        //}

        Random rnd;
        double minutes;
        public double DepartureDate()
        {
            DateTime start = DateTime.Now;
            rnd = new Random();
            //randomMinuts = Math.floor(Math.random() * 240) + 30;
            //var randomHours = Math.floor(randomMinuts / 60);
            //var randomMin = randomMinuts % 60;
          
            minutes = rnd.Next(1, 721);
          

            //DateTime value = start.AddMinutes(rnd.Next(720));

            //string time = value.ToString("HH:mm");

            return minutes;
        }




    }
}
