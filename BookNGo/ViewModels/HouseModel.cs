using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookNGo.Models;

namespace BookNGo.ViewModels
{
    public class HouseModel
    {
        public House House { get; set; }
        public List<int> SelectedFeatures { get; set; }

        public virtual List<Feature> Features { get; set; }

        public HouseModel()
        {

        }

        public HouseModel(House _House, List<Feature> _Features)
        {
            House = _House;
            Features = _Features;
            SelectedFeatures = new List<int>();
        }

    }
}