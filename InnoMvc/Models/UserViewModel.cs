﻿using System.ComponentModel;

namespace InnoMvc.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public string ADI { get; set; } = string.Empty;
        public string SOYADI { get; set; } = string.Empty;
        public string KULLANICI_ADI { get; set; } = string.Empty;
    }
}
