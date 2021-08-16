using Blog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Entities
{
    public class Post
    {

        public int Id { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public bool Published { get; set; }
        public DateTime PostedOn { get; set; }
        public DateTime? Modified { get; set; }

        public PostType Type { get; set; }

        // klucz obcy do Kategorii
        public int CategoryId { get; set; }
        // właściwość nawigacyjna
        public Category Category { get; set; }

        // klucz obcy do użytkownika
        public int UserId { get; set; }
        // właściwość nawigacyjna
        public User User { get; set; }


        // klucz obcy do użytkownika
        public int? ApprovedByUserId { get; set; }
        // właściwość nawigacyjna
        public User ApprovedBy { get; set; }


        // właściwość nawigacyjna
        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

    }
}
