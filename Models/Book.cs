using System;
using System.ComponentModel.DataAnnotations;
namespace ELibrary.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Format { get; set; }
        public string Language { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }
        public string Edition { get; set; }
        public bool Holding { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime HoldingDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }


        public int StudentId { get; set; }

    }
}
