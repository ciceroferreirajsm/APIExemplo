﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjetoExemploAPI.Model.Produtos;
using System;
using System.Threading.Tasks;

namespace ProjetoExemploAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ILogger<ProdutoController> _logger;

        private readonly IProdutoService _produtoService;

        public ProdutoController(ILogger<ProdutoController> logger, IProdutoService produtoService)
        {
            _logger = logger;
            _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
        }

        [Authorize(Policy = "Cicero")]
        [HttpPut("AdicionarProduto/{referencia}")]
        public async Task<IActionResult> AdicionarProduto(string referencia)
        {
            try
            {
                Produto produto = new Produto()
                {
                    Referencia = referencia
                };

                produto = await _produtoService.AdicionarProduto(produto);

                if(produto != null)
                {
                    return StatusCode(201);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Policy = "Cicero")]
        [HttpGet("ObterProduto/{Id}")]
        public async Task<IActionResult> ObterProduto(int IdProduto)
        {
            try
            {
                Produto retorno = await _produtoService.ObterPorId(IdProduto);

                if (retorno != null)
                {
                    return Ok(retorno);
                }
                else
                {
                    return BadRequest("Object was Not Found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Policy = "Cicero")]
        [HttpDelete("ExcluirProduto/{Id}")]
        public async Task<IActionResult> ExcluirProduto(int IdProduto)
        {
            try
            {
                bool produtoExcluido = await _produtoService.ExcluirProduto(IdProduto);

                if (produtoExcluido)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Object was Not Found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Policy = "Cicero")]
        [HttpPost("AtualizarProduto")]
        public async Task<IActionResult> AtualizarProduto(Produto produto)
        {
            try
            {
                Produto produtoAtualizado = await _produtoService.AtualizarProduto(produto);

                if (produtoAtualizado != null)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Object was Not Found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
