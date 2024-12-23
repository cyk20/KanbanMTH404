using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using global::KanbanBoard.DataAccess;
using Kanban.Domain.Entities;

namespace KanbanBoard.Business
{
    public class BoardService
    {
        private readonly KanbanDbContext _context;

        public BoardService(KanbanDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Board>> GetAllBoardsAsync()
        {
            return await _context.Boards.Include(b => b.Columns).ThenInclude(c => c.Cards).ToListAsync();
        }

        public async Task<Board> GetBoardByIdAsync(int id)
        {
            return await _context.Boards
                .Include(b => b.Columns)
                .ThenInclude(c => c.Cards)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Board> AddBoardAsync(Board board)
        {
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
            return board;
        }

        public async Task<bool> DeleteBoardAsync(int id)
        {
            var board = await _context.Boards.FindAsync(id);
            if (board == null) return false;

            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Board> UpdateBoardAsync(Board updatedBoard)
        {
            var existingBoard = await _context.Boards.FindAsync(updatedBoard.Id);
            if (existingBoard == null) return null;

            existingBoard.Name = updatedBoard.Name;
            await _context.SaveChangesAsync();
            return existingBoard;
        }
    }


}


