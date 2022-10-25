
using MongoDB.Driver;
using Models;



namespace CompanyAPI.Services
{
    public class CompanyServices
    {
      
            private readonly IMongoCollection<Company> _company;
     

            //public AnimalServices(IDatabaseSettings settings)
            //{
            //    var animal = new MongoClient(settings.ConnectionString);
            //    var database = animal.GetDatabase(settings.DatabaseName);
            //    _animals = database.GetCollection<Animal>(settings.AnimalCollectionName);
            //}

            //public Animal Create(Animal animal)
            //{
            //    _animals.InsertOne(animal);
            //    return animal;
            //}

            //public List<Animal> Get() => _animals.Find<Animal>(animal => true).ToList();

            //public Animal Get(string chip) => _animals.Find<Animal>(animal => animal.Chip == chip).FirstOrDefault();

            //public void Update(string chip, Animal animalIn)
            //{
            //    _animals.ReplaceOne(animal => animal.Chip == chip, animalIn);
            //}

            //public void Remove(Animal animalIn) => _animals.DeleteOne(animal => animal.Chip == animalIn.Chip);
      
    }
}
