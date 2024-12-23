using Kanban.Business;
using Kanban.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly CardService _cardService;

        public CardsController(CardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCard(Card card)
        {
            var createdCard = await _cardService.AddCardAsync(card);
            return CreatedAtAction(nameof(AddCard), new { id = createdCard.Id }, createdCard);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard(int id, Card card)
        {
            if (id != card.Id) return BadRequest();

            var updatedCard = await _cardService.UpdateCardAsync(card);
            if (updatedCard == null) return NotFound();

            return Ok(updatedCard);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var result = await _cardService.DeleteCardAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
