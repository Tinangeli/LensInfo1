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
        public int IDMovie { get; set; }
        public string MovieName { get; set; }
        public int AgeLimit { get; set; }
        public TimeSpan Duration { get; set; }
        public string Genre { get; set; }
        public DateTime DateAdded { get; set; }

        public byte[] PosterBytes { get; set; }

        public float Price { get; set; }
    }

    public class MovieData
    {
        private static MovieData _instance;

        public static MovieData Instance => _instance ??= new MovieData();

        public ObservableCollection<Movie> Movies { get; set; }

        private MovieData() // Constructor is private for singleton
        {
            Movies = new ObservableCollection<Movie>();
        }
    }
}
