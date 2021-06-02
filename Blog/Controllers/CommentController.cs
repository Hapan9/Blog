﻿using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            try
            {
                return Ok(await _commentService.GetAll());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _commentService.Get(id));
            }
            catch (CommentException ex)
            {
                return NotFound(ex.Message);
            }
        }
        //public async Task<IActionResult> GetCommentsByArticle([FromRoute] Guid articleId)
        //{
        //    try
        //    {

        //    }
        //    catch(CommentException ex)
        //    {
        //        return N
        //    }
        //}
        [Route("CreateComment/{ArticleId}")]
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromRoute]Guid ArticleId, [FromBody] CommentDTO commentDTO)
        {
            try
            {
                await _commentService.Create(ArticleId, commentDTO);
                return Ok();
            }
            catch (CommentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("UpdateComment/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromRoute] Guid id, [FromBody] CommentDTO commentDTO)
        {
            try
            {
                await _commentService.Update(id, commentDTO);
                return Ok();
            }
            catch (CommentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("DeleteComment/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
        {
            try
            {
                await _commentService.Delete(id);
                return Ok();
            }
            catch (CommentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
