using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryAPI.Data;
using LibraryAPI.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Services
{
    public class EfSqlBooks : ILookupBooks
    {
        private readonly LibraryDataContext _context;
        private readonly MapperConfiguration _config;
        private readonly IMapper _mapper;

        public EfSqlBooks(LibraryDataContext context, MapperConfiguration config, IMapper mapper)
        {
            _context = context;
            _config = config;
            _mapper = mapper;
        }

        public async Task<GetBooksResponse> GetAllBooks()
        {
            var books = await _context.GetBooksThatAreInInventory()
               .ProjectTo<GetBooksResponseItem>(_config)
               .ToListAsync();

            var response = new GetBooksResponse
            {
                Data = books
            };
            return response;
        }
    }
}
