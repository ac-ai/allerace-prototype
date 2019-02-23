using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllerAce_prototype_v2
{
    public class MeasurementEntry
    {
        public String Name { get; set; }
        public String Tags { get; set; }
        public String Allergen { get; set; }
        public int histamineLevel { get; set; }
        public DateTime dateAndTime { get; set; }

        public MeasurementEntry (String Name, String Tags, String Allergen, int histamineLevel, DateTime dateAndTime)
        {
            this.Name = Name;
            this.Tags = Tags;
            this.Allergen = Allergen;
            this.histamineLevel = histamineLevel;
            this.dateAndTime = dateAndTime;
        }

        public static List<MeasurementEntry> TrollMeasurementEntries()
        {
            return new List<MeasurementEntry>(new MeasurementEntry[4] {
                new MeasurementEntry("Casey Watson", "golfer, frequency quesadilla eater", "ass", 6969, new DateTime(2018, 11, 15, 1, 40, 0)),
                new MeasurementEntry("Scott Haskell", "suburban family, European, eats ass", "Americans", 420, new DateTime(2018, 11, 15, 4, 20, 0)),
                new MeasurementEntry("Adam Racette", "gay father of one, gets down on dance floors", "Monkeys", 350, new DateTime(2018, 11, 11, 1, 11, 0)),
                new MeasurementEntry("Chris Sedlak", "pagan, atheist, worships evil in all forms", "Pagans", 666, new DateTime(2018, 6, 6, 6, 06, 0)),
            });
        }

        public static List<MeasurementEntry> MeasurementEntries()
        {
            return new List<MeasurementEntry>(new MeasurementEntry[5] {
                new MeasurementEntry("Casey Watson", "five minutes after allergen introduced", "grass pollen", 450, new DateTime(2018, 11, 15, 1, 40, 0)),
                new MeasurementEntry("Casey Watson", "baseline measurement", "grass pollen", 100, new DateTime(2018, 11, 15, 1, 35, 0)),
                new MeasurementEntry("Scott Haskell", "", "cat hair", 100, new DateTime(2018, 11, 15, 4, 20, 0)),
                new MeasurementEntry("Adam Racette", "severe peanut allergy", "macadamia", 350, new DateTime(2018, 11, 11, 1, 11, 0)),
                new MeasurementEntry("Chris Sedlak", "vaccuum salesman", "dust pollen", 50, new DateTime(2018, 6, 6, 6, 06, 0)),
            });
        }
    }

    public class MeasurementEntryViewModel
    {
        private List<MeasurementEntry> measurementEntries;
        public List<MeasurementEntry> MeasurementEntries { get { return this.measurementEntries; } }

        public MeasurementEntryViewModel (List<MeasurementEntry> entries)
        {
            this.measurementEntries = entries;
        }
    }
}
