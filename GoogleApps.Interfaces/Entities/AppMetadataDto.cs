using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleApps.Interfaces.Entities
{    
    public class AppMetadataDto
    {
        public string title { get; set; }
        public string description { get; set; }
        public string descriptionHTML { get; set; }
        public string summary { get; set; }
        public string installs { get; set; }
        public int minInstalls { get; set; }
        public int maxInstalls { get; set; }
        public double score { get; set; }
        public string scoreText { get; set; }
        public int ratings { get; set; }
        public int reviews { get; set; }
        public Histogram histogram { get; set; }
        public int price { get; set; }
        public bool free { get; set; }
        public string currency { get; set; }
        public string priceText { get; set; }
        public bool offersIAP { get; set; }
        public object IAPRange { get; set; }
        public string size { get; set; }
        public string androidVersion { get; set; }
        public string androidVersionText { get; set; }
        public string developer { get; set; }
        public string developerId { get; set; }
        public string developerEmail { get; set; }
        public string developerWebsite { get; set; }
        public string developerAddress { get; set; }
        public string privacyPolicy { get; set; }
        public string developerInternalID { get; set; }
        public string genre { get; set; }
        public string genreId { get; set; }
        public object familyGenre { get; set; }
        public object familyGenreId { get; set; }
        public string icon { get; set; }
        public string headerImage { get; set; }
        public List<string> screenshots { get; set; }
        public object video { get; set; }
        public object videoImage { get; set; }
        public string contentRating { get; set; }
        public object contentRatingDescription { get; set; }
        public bool adSupported { get; set; }
        public object released { get; set; }
        public long updated { get; set; }
        public string version { get; set; }
        public string recentChanges { get; set; }
        public List<object> comments { get; set; }
        public bool editorsChoice { get; set; }
        public List<Feature> features { get; set; }
        public string appId { get; set; }
        public string url { get; set; }
    }
    public class Histogram
    {
        public int _1 { get; set; }
        public int _2 { get; set; }
        public int _3 { get; set; }
        public int _4 { get; set; }
        public int _5 { get; set; }
    }

    public class Feature
    {
        public string title { get; set; }
        public string description { get; set; }
    }

}