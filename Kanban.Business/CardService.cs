using Kanban.Domain.Entities;
using KanbanBoard.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Business
{
    public class CardService
    {
        private readonly KanbanDbContext _context;

        public CardService(KanbanDbContext context)
        {
            _context = context;
        }

        public async Task<Card> AddCardAsync(Card card)
        {
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task<bool> DeleteCardAsync(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null) return false;

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Card> UpdateCardAsync(Card updatedCard)
        {
            var existingCard = await _context.Cards.FindAsync(updatedCard.Id);
            if (existingCard == null) return null;

            existingCard.Title = updatedCard.Title;
            existingCard.Description = updatedCard.Description;
            existingCard.DueDate = updatedCard.DueDate;

            await _context.SaveChangesAsync();
            return existingCard;
        }
    }
}
