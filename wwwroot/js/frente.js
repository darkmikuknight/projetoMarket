/**
 * 
 */

const enderecoProduto = "https://localhost:5001/Produtos/Produto/"
let produto
let compra = []

//Funções//
function preencherFormulario(dadosProduto){

    $("#nome").val(dadosProduto.nome)
    $("#categoria").val(dadosProduto.categoria.nome)
    $("#fornecedor").val(dadosProduto.fornecedor.nome)
    $("#precoDeVenda").val(dadosProduto.precoDeVenda)
    $("#quantidade").val(dadosProduto.quantidade)
}

function zerarFormulario(){

    $("#nome").val("")
    $("#categoria").val("")
    $("#fornecedor").val("")
    $("#precoDeVenda").val("")
    $("#quantidade").val("")
    $("#codigoProduto").val("")
}

function adicionarTabela(p, quantidade){

    //Adicionando os produtos no carrinho de compras//
    const produtoTemp = {}
    Object.assign(produtoTemp, produto)
    compra.push(produtoTemp)

    if(p.medicao == 0)
        p.medicao = "l"
    else if(p.medicao == 1)
        p.medicao = "K"
    else if(p.medicao == 2)
        p.medicao = "U"
    else
        p.medicao = "U"
    $("#compras").append(`<tr>
            <td>${p.id}</td>
            <td>${p.nome}</td>
            <td>${quantidade}</td>
            <td>${p.precoDeVenda}R$</td>
            <td>${p.medicao}</td>
            <td>${p.precoDeVenda*quantidade}</td>
            <td><button class='btn btn-danger'>Remover</button></td>
        </tr>`)
}

$("#formProduto").on("submit", function(event){

    event.preventDefault()
    const produtoParaTabela = produto
    const quantidadeProduto = $("#quantidade").val()
    adicionarTabela(produtoParaTabela, quantidadeProduto)

    produto = null
    zerarFormulario()   
})



//Requeisição Ajax
$("#pesquisar").click(function(){

    const codProduto = $("#codigoProduto").val()

    $.post(enderecoProduto + codProduto, function(dados, status){
        produto = dados
        preencherFormulario(produto)

    }).fail(function(){
        alert("Produto inválido")
    })
})
