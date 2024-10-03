using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensInfo1
{
    public class Movie
    {
        public int IDMovie {  get; set; }

        public string MovieName { get; set; }

        public int AgeLimit { get; set; }

        public TimeSpan Duration { get; set; }

        public string MovieDescription { get; set; }

        public DateTime DateAdded { get; set; }
    }

    public class MovieData
    {
        public static LensInfo1.MovieData _instance;

        public static LensInfo1.MovieData Instance => _instance ??= new LensInfo1.MovieData();

        public ObservableCollection<Movie> Movies { get; set; }

        public MovieData()
        {
            Movies = new ObservableCollection<Movie>();
        }
    }
}
