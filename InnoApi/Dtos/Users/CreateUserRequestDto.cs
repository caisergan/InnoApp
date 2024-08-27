namespace InnoApi.Dtos.Users
{
    public class CreateUserRequestDto
    {
        public int ID { get; set; }
        public string ADI { get; set; } = string.Empty;
        public string SOYADI { get; set; } = string.Empty;
        public string KULLANICI_ADI { get; set; } = string.Empty;
    }
}
