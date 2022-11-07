# Banco-Master
Teste técnico para vaga desenvolvedor jr Banco Master

Informações da API:
- Entity Framework como ORM;
- SQL/mySQL como banco de dados;
- Swagger para documentação.

Endpoints:

Customer

/api/Cliente/CadastrarCliente - Cadastrar novos clientes;
/api/Cliente - Consultar todos clientes cadastrados;
/api/Cliente/ConsultarCliente/CPF/{cpf} - Consulta cliente por CPF.
/api/Cliente/ConsultarCliente/PIX/{PIX} - Consulta cliente pela chave PIX.

Transfer

/api/Transferencia - Carregar extrato da chave pix; (O método pelo swagger retorna com o ID do pix de origem e destino como null, mas funciona corretamente no banco de dados)

/api/Transferencia/TransferirPix - Iniciar nova transferência por chave PIX. (Quando executado gera uma cópia dos usuários antes de atualizar o valor da transfêrencia,
isso é um feature não intencional porém não afeta a execução da API.)

