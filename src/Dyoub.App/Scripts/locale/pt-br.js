﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {
    moment.defineLocale('pt-br', {
        months: 'Janeiro_Fevereiro_Março_Abril_Maio_Junho_Julho_Agosto_Setembro_Outubro_Novembro_Dezembro'.split('_'),
        monthsShort: 'Jan_Fev_Mar_Abr_Mai_Jun_Jul_Ago_Set_Out_Nov_Dez'.split('_'),
        weekdays: 'Domingo_Segunda-feira_Terça-feira_Quarta-feira_Quinta-feira_Sexta-feira_Sábado'.split('_'),
        weekdaysShort: 'Dom_Seg_Ter_Qua_Qui_Sex_Sáb'.split('_'),
        weekdaysMin: 'Do_2ª_3ª_4ª_5ª_6ª_Sá'.split('_'),
        weekdaysParseExact: true,
        longDateFormat: {
            LT: 'HH:mm',
            LTS: 'HH:mm:ss',
            L: 'DD/MM/YYYY',
            LL: 'D [de] MMMM [de] YYYY',
            LLL: 'D [de] MMMM [de] YYYY [às] HH:mm',
            LLLL: 'dddd, D [de] MMMM [de] YYYY [às] HH:mm'
        },
        calendar: {
            sameDay: '[Hoje às] LT',
            nextDay: '[Amanhã às] LT',
            nextWeek: 'dddd [às] LT',
            lastDay: '[Ontem às] LT',
            lastWeek: function () {
                return (this.day() === 0 || this.day() === 6) ?
                    '[Último] dddd [às] LT' : // Saturday + Sunday
                    '[Última] dddd [às] LT'; // Monday - Friday
            },
            sameElse: 'L'
        },
        relativeTime: {
            future: 'em %s',
            past: '%s atrás',
            s: 'poucos segundos',
            m: 'um minuto',
            mm: '%d minutos',
            h: 'uma hora',
            hh: '%d horas',
            d: 'um dia',
            dd: '%d dias',
            M: 'um mês',
            MM: '%d meses',
            y: 'um ano',
            yy: '%d anos'
        },
        dayOfMonthOrdinalParse: /\d{1,2}º/,
        ordinal: '%dº'
    });

    function locale($provide) {
        var PLURAL_CATEGORY = { ZERO: "zero", ONE: "one", TWO: "two", FEW: "few", MANY: "many", OTHER: "other" };

        $provide.value("$locale", {
            id: "pt-br",
            DATETIME_FORMATS: {
                MONTH: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
                day: "DD",
                weekDay: "dddd",
                month: 'MM',
                monthName: 'MMMM',
                fullDate: "dddd, DD [de] MMMM [de] YYYY",
                longDate: "DD [de] MMMM [de] YYYY",
                medium: "D [de] MMM [de] YY HH:mm:ss",
                mediumDate: "D [de] MMM [de] YY",
                mediumTime: "HH:mm:ss",
                short: "DD/MM/YYYY HH:mm",
                shortDate: "DD/MM/YYYY",
                shortTime: "HH:mm"
            },
            NUMBER_FORMATS: {
                CURRENCY_SYM: "R$ ",
                DECIMAL_SEP: ",",
                GROUP_SEP: ".",
                PATTERNS: [
                  { gSize: 3, lgSize: 3, maxFrac: 3, minFrac: 0, minInt: 1, negPre: "-", negSuf: "", posPre: "", posSuf: "" },
                  { gSize: 3, lgSize: 3, maxFrac: 2, minFrac: 2, minInt: 1, negPre: "\u00a4-", negSuf: "", posPre: "\u00a4", posSuf: "" }
                ]
            },
            pluralCat: function (n, opt_precision) {
                return (n >= 0 && n <= 2 && n != 2) ? PLURAL_CATEGORY.ONE : PLURAL_CATEGORY.OTHER;
            },
            translation: {
                "true": "Sim",
                "false": "Não",
                "0": "Por favor verifique a conexão com a internet e tente novamente.",
                "-1": "Por favor verifique a conexão com a internet e tente novamente.",
                "401": "Usuário e/ou senha inválidos.",
                "403": "Acesso negado.",
                "404": "Não foi possível estabelecer uma comunicação com o servidor.",
                "500": "Ocorreu um problema. Por favor tente novamente.",
                "Access account": "Acessar conta",
                "Accessing": "Acessando",
                "Account": "Conta",
                "Active": "Ativo",
                "Add": "Adicionar",
                "Add items": "Adicionar itens",
                "Add payment": "Adicionar pagamento",
                "Add products": "Adicionar produtos",
                "Add store": "Adicionar loja",
                "Add sale fee": "Adicionar taxa de venda",
                "Additional information": "Informações adicionais",
                "All items": "Todos os itens",
                "All stores": "Todas as lojas",
                "Already have an account?": "Já possui uma conta?",
                "Alternative phone number": "Telefone alternativo",
                "Amount": "Quantidade",
                "Anytime you want.": "A <strong>qualquer momento</strong>.",
                "Anywhere you like.": "De <strong>qualquer lugar</strong>.",
                "Are you sure you want confirm this purchase order?": "Deseja realmente confirmar essa ordem de compra?",
                "Are you sure you want confirm this sale order?": "Deseja realmente confirmar essa ordem de venda?",
                "Are you sure you want erase all prices?": "Deseja realmente apagar todos os preços?",
                "Are you sure you want revert this purchase order?": "Deseja realmente estornar essa ordem de compra?",
                "Are you sure you want revert this sale order?": "Deseja realmente estornar essa ordem de venda?",
                "Are you sure you want to delete?": "Deseja realmente remover?",
                "Are you sure you want to permanently delete?": "Deseja realmente remover permanentemente?",
                "Are you sure you want undo the changes?": "Deseja realmente desfazer as alterações?",
                "At least one item must be informed.": "Pelo menos um item deve ser informado.",
                "At least one payment must be added.": "Pelo menos um pagamento deve ser adicionado.",
                "At least one product must be added.": "Pelo menos um produto deve ser adicionado.",
                "Balance": "Saldo",
                "Bank reconciliation": "Conciliação bancária",
                "Billed": "Faturado",
                "Billed amount": "Total faturado",
                "Billed value": "Valor faturado",
                "Budget": "Orçamento",
                "Cancel": "Cancelar",
                "Cancel account": "Cancelar conta",
                "Cancelling your account will permanently remove all your data and your access to the application.":
                    "Cancelar sua conta removerá permanentemente todos os seus dados e seu acesso ao aplicativo.",
                "Confirm": "Confirmar",
                "Cannot copy from the same store.": "Não é possível copiar da mesma loja.",
                "Cannot have one or more fees with the same installment condition.":
                    "Não é possível ter uma ou mais taxas com a mesma condição de parcelamento.",
                "Cannot confirm sale order with pending payment.":
                    "Não é possível confirmar ordem de venda com pagamento pendente.",
                "Can fraction": "Permite fracionar",
                "Cash flow": "Fluxo de caixa",
                "Cash activity details": "Detalhes do lançamento",
                "Cash activity not found.": "Lançamento não encontrado",
                "Catalog": "Catálogo",
                "Change password": "Alterar senha",
                "Changes undone.": "Alterações desfeitas.",
                "Clear filter": "Limpar filtro",
                "Clear form": "Limpar formulário",
                "Code": "Código",
                "Collaborators": "Colaboradores",
                "Commercial": "Comercial",
                "Confirmed": "Confirmado",
                "Contract total value": "Valor total do contrato",
                "Copy": "Copiar",
                "Copying": "Copiando",
                "Copy from pricing table": "Copiar de outra tabela",
                "Copy to pricing table": "Copiar para tabela de preços",
                "Create account": "Criar conta",
                "Credit": "Crédito",
                "Customer": "Cliente",
                "Customer details": "Detalhes do cliente",
                "Customer not identified": "Cliente não identificado",
                "Customer not found.": "Cliente não encontrado",
                "Customer name": "Nome do cliente",
                "Customers": "Clientes",
                "Dangerous zone": "Área crítica",
                "Daily": "Diário",
                "Date": "Data",
                "day after payment": "dia após o pagamento",
                "days after payment": "dias após o pagamento",
                "Days for renewal": "Dias para renovação",
                "Debit": "Débito",
                "Description": "Descrição",
                "Developing your business.": "Desenvolvendo seu negócio.",
                "Discount": "Desconto",
                "Discounted percentage": "Percentual descontado",
                "Discounted value": "Valor descontado",
                "Don't have an account?": "Não possui uma conta?",
                "Do everything online.": "Faça tudo <strong>online</strong>.",
                "Draft": "Rascunho",
                "Early receipt": "Recebimento antecipado",
                "It indicates if all the installments are received in a single time (early receipt) or according to the installment payment (received per installment).":
                    "Indica se todas as parcelas são recebidas em uma única vez (recebimento antecipado) ou conforme o parcelamento (recebimento por parcela).",
                "Edit": "Editar",
                "Editing": "Editando",
                "Edit cash activity": "Editar lançamento",
                "Edit customer": "Editar cliente",
                "Edit fixed expense": "Editar despesa fixa",
                "Edit payment method": "Editar método de pagamento",
                "Edit product": "Editar produto",
                "Edit purchase order": "Editar ordem de compra",
                "Edit sale order": "Editar ordem de venda",
                "Edit service": "Editar serviço",
                "Edit store": "Editar loja",
                "Edit supplier": "Editar fornecedor",
                "Edit wallet": "Editar carteira",
                "Email": "E-mail",
                "Email not found.": "E-mail não encontrado.",
                "End date": "Data de término",
                "Erase prices": "Apagar preços",
                "Evaluate your cash flow in real time.": "Avalie seu fluxo de caixa em tempo real.",
                "Fee percentage or fee fixed value must be informed.": "O percentual ou o valor fixo da taxa deve ser informado.",
                "Financial": "Financeiro",
                "Fixed expense details": "Detalhes da despesas fixa",
                "Fixed expense not found.": "Despesas fixa não encontrada",
                "Fixed expenses": "Despesas fixas",
                "Fixed expenses, receipt of credit card, etc.": "Despesas fixas, vendas com cartão de crédito, etc.",
                "From": "A partir de",
                "Full name": "Nome completo",
                "General": "Geral",
                "Go back": "Voltar",
                "Go back to signin": "Voltar para o login",
                "Go back to dashboard": "Voltar para o dashboard",
                "Go to customer details": "Ir para detalhes do cliente",
                "Go to items details": "Ir para itens da ordem de venda",
                "Go to payment details": "Ir para detalhes do pagamento",
                "Go to sale order details": "Ir para detalhes da ordem de venda",
                "I forgot my password": "Esqueci minha senha",
                "Inactive": "Inativo",
                "Incoming, outcoming and transfer orders.": "Ordens de entrada, saída e transferência.",
                "Incomes and expenses": "Receitas e despesas",
                "Installments": "Parcelas",
                "Installment option": "Opção de parcelamento",
                "Installment value": "Valor da parcela",
                "Inventory": "Estoque",
                "Invite people to work with you.": "Convide pessoas para trabalhar com você.",
                "Invoice number": "Número da nota fiscal",
                "Is manufactured": "É manufaturado",
                "Issue date": "Data da emissão",
                "Item already added.": "Item já adicionado.",
                "Item name or code": "Nome ou código do item",
                "Item type": "Tipo do item",
                "Items": "Itens",
                "Installment condition": "Condição de parcelamento",
                "Last change password:": "Última alteração de senha:",
                "Last login:": "Último login:",
                "Loading": "Carregando",
                "Location": "Local",
                "Management": "Gerenciamento",
                "Manage mulltiple shops in one place.": "Gerencie múltiplas lojas em um só lugar.",
                "Manually entered": "Informado manualmente",
                "Make sure the fields are filled correctly.": "Verifique se os campos foram preenchidos corretamente.",
                "Marketed": "Comercializado",
                "Month": "Mês",
                "Monthly": "Mensal",
                "Monthly payment,": "Pagamento mensal,",
                "More much...": "Muito mais...",
                "More results": "Mais resultados",
                "Multistore": "Controle de filiais",
                "My profile": "Meu perfil",
                "Name": "Nome",
                "National ID": "CPF/CNPJ",
                "New customer": "Novo cliente",
                "New fixed expense": "Nova despesa fixa",
                "New messages": "Novas mensages",
                "New cash activity": "Novo lançamento",
                "New payment method": "Novo método de pagamento",
                "New password": "Nova senha",
                "New product": "Novo produto",
                "New purchase order": "Nova ordem de compra",
                "New rent contract": "Novo contrato de locação",
                "New sale order": "Nova ordem de venda",
                "New service": "Novo serviço",
                "New store": "Nova loja",
                "New supplier": "Novo fornecedor",
                "New wallet": "Nova carteira",
                "No": "Não",
                "No code": "Sem código",
                "No payment added.": "Nenhum pagamento adicionado.",
                "No sale fees": "Sem taxa de venda.",
                "No records found": "Nenhum registro encontrado",
                "Not defined": "Não definido",
                "Not found what you’re looking for?": "Não achou o que estava procurando?",
                "Not marketed": "Não comercializado",
                "Number of installments": "Número de parcelas",
                "of": "de",
                "Old password": "Senha anterior",
                "Old password is incorrect.": "Senha anterior está incorreta.",
                "One or more items have total negative.": "Um ou mais itens têm total negativo.",
                "One or more products have total negative.": "Um ou mais produtos têm total negativo.",
                "One or more products with insufficient balance.": "Um ou mais produtos com estoque insuficiente.",
                "Only products": "Somente produtos",
                "Only services": "Somente serviços",
                "Operation": "Operação",
                "Organize your catalog and pricing table.": "Organize seu catálogo e tabela de preços.",
                "Other cash activities": "Outros lançamentos",
                "Other credits": "Outros créditos",
                "Other debits": "Outros débitos",
                "Other movements": "Outros movimentos",
                "Other taxes": "Outras taxas",
                "Payment": "Pagamento",
                "Payment date": "Data do pagamento",
                "Payment method": "Método de pagamento",
                "Payment methods": "Métodos de pagamento",
                "Payment method details": "Detalhes do método de pagamento",
                "Payment method not found.": "Método de pagamento não encontrado",
                "Payments": "Pagamentos",
                "Password": "Senha",
                "Pending changes": "Alterações pendentes",
                "Phone number": "Telefone",
                "Press enter to add the first item.": "Pressione enter para adicionar o primeiro item.",
                "Press enter to add the first product.": "Pressione enter para adicionar o primeiro produto.",
                "Prices erased.": "Preços apagados.",
                "Pricing": "Preço",
                "Pricing table": "Tabela de preços",
                "Pricing tables": "Tabelas de preços",
                "Pricing table not found.": "Tabela de preços não encontrada",
                "Privacy": "Privacidade",
                "Product": "Produto",
                "Products": "Produtos",
                "Product already added.": "Produto já adicionado",
                "Product details": "Detalhes do produto",
                "Product and services": "Produtos e serviços",
                "Product name or code": "Código ou nome do produto",
                "Product not found.": "Produto não encontrado",
                "Product stock": "Estoque de produtos",
                "Profile": "Perfil",
                "Purchase, sale and rent operations.": "Operações de compra, venda e aluguel.",
                "Purchase order": "Ordem de compra",
                "Purchase orders": "Ordens de compra",
                "Purchase total": "Total da compra",
                "Purchase order not found.": "Ordem de compra não encontrada",
                "Purchases": "Compras",
                "Read carefully our": "Leia atentamente nossos",
                "Receivables": "Recebíveis",
                'Received date': 'Data do recebimento',
                "Receivables anticipation": "Antecipação de recebíveis",
                "Records": "Registros",
                "Recovery token expired.": "Token de recuperação expirado.",
                "Remove": "Remover",
                "Removing": "Removendo",
                "Renewals": "Renovações",
                "Rent contracts": "Contratos de locação",
                "Rent contract": "Contrato de locação",
                "Rent contract not found.": "Contrato de locação não encontrado.",
                "Reset": "Resetar",
                "Revert": "Estornar",
                "Reverting": "Estornando",
                "Sale fee": "Taxa de venda",
                "Sale fees": "Taxas de venda",
                "Sale order": "Ordem de venda",
                "Sale order already confirmed.": "Ordem de venda já confirmada.",
                "Sale order details": "Detalhes da ordem de venda",
                "Sale order not found.": "Ordem de venda não encontrada.",
                "Sale orders": "Ordens de venda",
                "Sale total": "Total da venda",
                "Sales": "Vendas",
                "Same payment date": "Mesma data do pagamento",
                "Save": "Salvar",
                "Saving": "Salvando",
                "Search": "Pesquisar",
                "Search filter": "Fitro de pesquisa",
                "Searching": "Pesquisando",
                "Searching...": "Pesquisando...",
                "See for yourself what is coming.": "Veja você mesmo o que ainda está por vir.",
                "Select": "Selecionar",
                "Send": "Enviar",
                "Sending": "Enviar",
                "Sent": "Enviado",
                "Service": "Serviço",
                "Service details": "Detalhes do serviço",
                "Service name or code": "Código ou nome do serviço",
                "Service not found.": "Serviço não encontrado",
                "Services": "Serviços",
                "Shipping cost": "Frete",
                "Sign up with one free user in the first month.": "Crie uma conta com um usuário grátis no primeiro mês.",
                "Sign in": "Entrar",
                "Sign up": "Cadastrar",
                "Simple.": "Simples.",
                "Since": "Desde",
                "Social networks": "Redes sociais",
                "Solutions": "Soluções",
                "Solutions we have for you.": "Soluções que temos para você.",
                "Start date": "Data de início",
                "Stock": "Estoque",
                "Stock movement": "Movimenta estoque",
                "Stock transfer orders": "Ordens de transferência",
                "Store details": "Detalhes da loja",
                "Store not found.": "Loja não encontrada",
                "Store": "Loja",
                "Stores": "Lojas",
                "Supplier": "Fornecedor",
                "Supplier details": "Detalhes do fornecedor",
                "Supplier name": "Nome do fornecedor",
                "Supplier not found.": "Fornecedor não encontrado.",
                "Supplier not identified": "Fornecedor não identificado",
                "Suppliers": "Fornecedores",
                "Teams": "Equipes",
                "Team management": "Gerencie sua equipe",
                "Technical support": "Suporte técnico",
                "Tell us about it.": "Conte-nos um pouco sobre isso.",
                "Terms of use": "Termos de uso",
                "The customer data will be updated.": "Os dados deste cliente serão atualizados.",
                "The supplier data will be updated.": "Os dados deste fornecedor serão atualizados.",
                "There is no changes.": "Não há alterações.",
                "There is not fee": "Não há taxa",
                "There is no stores yet.": "Não há lojas ainda.",
                "This customer has associated sale orders.": "Este cliente tem ordens de vendas associadas.",
                "This month": "Este mês",
                "This purchase order has no products.": "Esta ordem de compra não possui produtos.",
                "This sale order has no items.": "Esta ordem de venda não possui itens.",
                "This store has associated fixed expenses.": "Essa loja possui despesas fixas associadas.",
                "This store has associated sale orders.": "Essa loja possui ordens de vendas associadas.",
                "This wallet has associated sales orders.": "Essa carteira possui ordens de vendas associadas.",
                "to": "a",
                "Today": "Hoje",
                "Total available": "Total disponível",
                "Total cost": "Custo total",
                "Total paid": "Total pago",
                "Total payable": "Total a pagar",
                "Transfer orders": "Ordens de transferência",
                "Try again": "Tentar novamente",
                "Update profile": "Atualizar perfil",
                "Undo changes": "Desfazer alterações",
                "Unkown error.": "Erro desconhecido.",
                "Unit cost": "Custo unitário",
                "Unit price": "Preço unitário",
                "Until": "Até",
                "user": "usuário",
                "Users": "Usuários",
                "Value": "Valor",
                "Wallet": "Carteira",
                "Wallet details": "Detalhes da carteira",
                "Wallet not found.": "Carteira não encontrada.",
                "Wallets": "Carteiras",
                "We are excited to help your business grow.": "Estamos animados em ajudar seu negócio a crescer.",
                "We will send you a confirmation": "Nós te enviaremos uma confirmação",
                "with one user free in the first month.": "com um usuário grátis no primeiro mês.",
                "Year": "Ano",
                "Yes": "Sim"
            }
        });
    }

    angular.module("ngLocale", [], [
        "$provide",
        locale
    ]);
})();
