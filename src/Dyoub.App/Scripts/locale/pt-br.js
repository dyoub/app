// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

(function () {
    function locale($provide) {
        var PLURAL_CATEGORY = { ZERO: "zero", ONE: "one", TWO: "two", FEW: "few", MANY: "many", OTHER: "other" };

        $provide.value("$locale", {
            id: "pt-br",
            DATETIME_FORMATS: {
                AMPMS: ["AM", "PM"],
                DAY: ["Domingo", "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira", "Sábado"],
                MONTH: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
                SHORTDAY: ["DOM", "SEG", "TER", "QUA", "QUI", "SEX", "SÁB"],
                SHORTMONTH: ["jan", "fev", "mar", "abr", "mai", "jun", "jul", "ago", "set", "out", "nov", "dez"],
                fullDate: "EEEE, d 'de' MMMM 'de' y",
                longDate: "d 'de' MMMM 'de' y",
                medium: "d 'de' MMM 'de' y HH:mm:ss",
                mediumDate: "d 'de' MMM 'de' y",
                mediumTime: "HH:mm:ss",
                short: "dd/MM/yyyy HH:mm",
                shortDate: "dd/MM/yyyy",
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
                "Already have an account?": "Já possui uma conta?",
                "Anytime you want.": "A <strong>qualquer momento</strong>.",
                "Anywhere you like.": "De <strong>qualquer lugar</strong>.",
                "Commercial": "Comercial",
                "Create account": "Criar conta",
                "Developing your business.": "Desenvolvendo seu negócio.",
                "Don't have an account?": "Não possui uma conta?",
                "Do everything online.": "Faça tudo <strong>online</strong>.",
                "Email": "E-mail",
                "Email not found.": "E-mail não encontrado.",
                "Evaluate your cash flow in real time.": "Avalie seu fluxo de caixa em tempo real.",
                "Financial": "Financeiro",
                "Fixed expenses, receipt of credit card, etc.": "Despesas fixas, vendas com cartão de crédito, etc.",
                "Full name": "Nome completo",
                "Go back to signin": "Voltar para o login",
                "Go back to dashboard": "Voltar para o dashboard",
                "I forgot my password": "Esqueci minha senha",
                "Incoming, outcoming and transfer orders.": "Ordens de entrada, saída e transferência.",
                "Incomes and expenses": "Receitas e despesas",
                "Inventory": "Estoque",
                "Invite people to work with you.": "Convide pessoas para trabalhar com você.",
                "Read carefully our": "Leia atentamente nossos",
                "Manage mulltiple shops in one place.": "Gerencie múltiplas lojas em um só lugar.",
                "Make sure the fields are filled correctly.": "Verifique se os campos foram preenchidos corretamente.",
                "Monthly payment,": "Pagamento mensal,",
                "More much...": "Muito mais...",
                "Multistore": "Controle de filiais",
                "New password": "Nova senha",
                "Not found what you’re looking for?": "Não achou o que estava procurando?",
                "Organize your catalog and pricing table.": "Organize seu catálogo e tabela de preços.",
                "Password": "Senha",
                "Pricing": "Preço",
                "Privacy": "Privacidade",
                "Product and services": "Produtos e serviços",
                "Purchase, sale and rent operations.": "Operações de compra, venda e aluguel.",
                "Recovery token expired.": "Token de recuperação expirado.",
                "Reset": "Resetar",
                "See for yourself what is coming.": "Veja você mesmo o que ainda está por vir.",
                "Send": "Enviar",
                "Sending": "Enviar",
                "Sent": "Enviado",
                "Sign up with one free user in the first month.": "Crie uma conta com um usuário grátis no primeiro mês.",
                "Sign in": "Entrar",
                "Sign up": "Cadastrar",
                "Simple.": "Simples.",
                "Solutions": "Soluções",
                "Solutions we have for you.": "Soluções que temos para você.",
                "Team management": "Gerencie sua equipe",
                "Tell us about it.": "Conte-nos um pouco sobre isso.",
                "Terms of use": "Termos de uso",
                "user": "usuário",
                "We are excited to help your business grow.": "Estamos animados em ajudar seu negócio a crescer.",
                "We will send you a confirmation": "Nós te enviaremos uma confirmação",
                "with one user free in the first month.": "com um usuário grátis no primeiro mês."
            }
        });
    }

    angular.module("ngLocale", [], [
        "$provide",
        locale
    ]);
})();
