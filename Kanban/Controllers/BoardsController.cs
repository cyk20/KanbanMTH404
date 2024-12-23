using Kanban.Domain.Entities;
using KanbanBoard.Business;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardsController : ControllerBase
    {
        private readonly BoardService _boardService;

        public BoardsController(BoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBoards()
        {
            var boards = await _boardService.GetAllBoardsAsync();
            return Ok(boards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoardById(int id)
        {
            var board = await _boardService.GetBoardByIdAsync(id);
            if (board == null) return NotFound();
            return Ok(board);
        }

        [HttpPost]
        public async Task<IActionResult> AddBoard(Board board)
        {
            var createdBoard = await _boardService.AddBoardAsync(board);
            return CreatedAtAction(nameof(GetBoardById), new { id = createdBoard.Id }, createdBoard);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBoard(int id, Board board)
        {
            if (id != board.Id) return BadRequest();

            var updatedBoard = await _boardService.UpdateBoardAsync(board);
            if (updatedBoard == null) return NotFound();

            return Ok(updatedBoard);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            var result = await _boardService.DeleteBoardAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
