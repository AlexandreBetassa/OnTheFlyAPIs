﻿using System.Collections.Generic;
using CompanyAPI.DatabaseSettings;
using Models;
using MongoDB.Driver;

namespace CompanyAPI.Services
{
    public class CompanyService
    {
        private readonly IMongoCollection<Company> _companies;
        //private readonly IMongoCollection<Address> _address;

        public CompanyService(IDatabaseSetting settings)
        {
            var animal = new MongoClient(settings.ConnectionString);
            var database = animal.GetDatabase(settings.DatabaseName);
            _companies = database.GetCollection<Company>(settings.CompanyCollectionName);
        }

        public Company Create(Company company){ _companies.InsertOne(company); return company; }
       
        public List<Company> Get() => _companies.Find<Company>(company => true).ToList();

        public Company Get(string cnpj) => _companies.Find(company => company.CNPJ == cnpj).FirstOrDefault();

        public void Update(string id, Company CompanyIn) => _companies.ReplaceOne(person => person.Id == id, CompanyIn);

        public void Delete(string cnpj)=>_companies.DeleteOne(person => person.CNPJ == cnpj);
    }
}
