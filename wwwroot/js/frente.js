/**
 * 
 */

const enderecoProduto = "https://localhost:5001/Produtos/Produto/"
const enderecoGerarVenda = "https://localhost:5001/Produtos/GerarVenda"
let produto
let compra = []
let __totalVenda__ = 0.0

//Inicialização//
atualizarTotal()
$("#posVenda").hide()

//Funções//
function atualizarTotal(){
    $("#totalVenda").html(__totalVenda__)
}

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
    const venda = {produto: p, quantidade: parseInt(quantidade), subtotal: p.precoDeVenda*quantidade}

    __totalVenda__ += venda.subtotal
    atualizarTotal()

    compra.push(venda)

    p.medicao = "U"

    if(p.medicao == 0)
        p.medicao = "l"
    else if(p.medicao == 1)
        p.medicao = "K"
    else if(p.medicao == 2)
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
    const produtoParaTabela = {}
    Object.assign(produtoParaTabela, produto)
    const quantidadeProduto = $("#quantidade").val()

    adicionarTabela(produtoParaTabela, quantidadeProduto)
    produto = null
    zerarFormulario()
})



//Requeisição Ajax//
$("#pesquisar").click(function(){

    const codProduto = $("#codigoProduto").val()

    $.post(enderecoProduto + codProduto, function(dados, status){
        produto = dados
        preencherFormulario(produto)

    }).fail(function(){
        alert("Produto inválido")
    })
})

//Finalização de Venda//
$("#finalizarVendaBtn").click(function(){
    if(__totalVenda__ <= 0){
        alert("Compra inválida! Nenhum produto adicionado.")
        return
    }
    
    const valorPago = $("#valorPago").val()
    
    if(!isNaN(valorPago)){
        if(valorPago >= __totalVenda__){
            const valorTroco = valorPago - __totalVenda__
            $("#valorTroco").val(valorTroco)
            $("#posVenda").show()
            $("#preVenda").hide()
            $("#valorPago").prop("disabled", true)

            //Processar array de compra//
            compra.forEach(item =>{
                item.produto = item.produto.id
            })

            //Preparar um novo objeto para enviar para o back-end//
            const venda = {
                total: __totalVenda__, 
                troco: valorTroco, 
                produtos: compra
            }
            
            //Enviar os dados para o back-end//
            $.ajax({ //faz requisição para qualquer verbo HTTP
                type: "POST",
                url: enderecoGerarVenda,
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify(venda),
                success: function(data){
                    console.log("Dados enviados com sucesso!")
                    console.log(data)
                }
            }) 

        }
        else{
            alert("Valor pago não pode ser menor que o valor total da compra!")
            return
        }

    }
    else{
        alert("Valor pago inválido!")
        return
    }
})

function restaurarModal(){
    $("#valorTroco").val("")
    $("#valorPago").val("")
    $("#posVenda").hide()
    $("#preVenda").show()
    $("#valorPago").prop("disabled", false)
    __totalVenda__ = 0.0
    atualizarTotal()
}

$("#fecharVenda").click(function(){
    restaurarModal()
})