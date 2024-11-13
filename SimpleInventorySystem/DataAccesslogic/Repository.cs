using Microsoft.EntityFrameworkCore;
using SimpleInventorySystem.DataLayer;
using SimpleInventorySystem.DataLayer.Models;
using SimpleInventorySystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInventorySystem.DataAccesslogic
{
    // implementing of IRepository interface  (Repository Design Pattern)
    public class Repository<T> : IRepository<T> where T : class
    {
        // system context
        private readonly InventoryContext Context;
        // list of entity of type T
        protected readonly DbSet <T> List;

        public Repository(InventoryContext _context)
        {
            this.Context = _context;
            this.List= this.Context.Set<T>();
        }

        // get all records and if the list is empty the function will throw an exception to be handled
        public async Task< IEnumerable<T>> GetAllAsync()
        {
           
            if ( await this.List.AnyAsync())
            {
                return  this.List;
            }
            else
            {

                throw new Exception($" there is no {(typeof(T)).ToString().Split(new string[] { "." }, StringSplitOptions.None).LastOrDefault()}s in the Iventory ( Empty Iventory ). ");
            }

        }


        public async Task<T> GetOneAsync(int id)
        {
            T entity = await this.List.FindAsync(id);
            if (entity == null)
            {
                throw new Exception($" there is no record in the Iventory with the Id: {id} ");
            }
            return entity;
        }

        //Add record
        public async Task AddAsync(T entity)
        {
            try
            {
                await this.List.AddAsync(entity);
                await this.Context.SaveChangesAsync();
            }catch (Exception ex)
            {
                throw new Exception((" ********** InnerException *************\r\n" + ex.InnerException.GetBaseException()).Split(new string[] { "The statement has been terminated." }, StringSplitOptions.None)[0]);
            }
        }

        //Update record
        public async Task UpdateAsync(T entity)
        {
            try
            {
                 this.List.Update(entity);
                // this.List.Update(entity);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception((" ********** InnerException *************\r\n" + ex.InnerException.GetBaseException()).Split(new string[] { "The statement has been terminated." }, StringSplitOptions.None)[0]);
            }
        }
    }
}
