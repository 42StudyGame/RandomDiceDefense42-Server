using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RandomDice_Login.Models
{
    public class Book_Maria
    {
        public string id { get; set; }
        public string bookName { get; set; }
        public int price { get; set; }
        public string category { get; set; }
        public string author { get; set; }

    }
    public class Book
    {
        [BsonId] // Id를 기본키로 둔다.
        [BsonRepresentation(BsonType.ObjectId)] // String형태를 ObjectId로 변환함.
        public string Id { get; set; }
        //public string? Id { get; set; }


        [BsonElement("Name")] // 데이터에 특성 추가. BookName은 Name속성에 매칭.
        public string BookName { get; set; } = null!;

        public decimal Price { get; set; }

        public string Category { get; set; } = null!;

        public string Author { get; set; } = null!;
    }
}
