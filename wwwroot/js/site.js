
//var form = document.getElementById('fomrInsert');
//var errorMessage = document.getElementById('error-message');

//form.addEventListener('submit', function(event) {
//    if (!form.checkValidity()) {
//        event.preventDefault();
//        errorMessage.textContent = 'Por favor, preencha todos os campos.';
//        errorMessage.style.display = 'block';
//    }
//});
function validarCPF() {
    event.preventDefault()

    var cpf = document.getElementById('cpfInput').value;
    var data = document.getElementById('dateInput').value;

    cpf = cpf.replace(/\D/g, '');

    var cpfValido = isValidCPF(cpf);
    var dataValida = isDataValida(data);

    if (cpfValido && dataValida) {
        document.getElementById('fomrInsert').submit();
    } else if (!dataValida) {
        alert('Data inválido');
    } else {
        alert('CPF inválido');
    }
}
function isDataValida(data) {
    var dataMinima = new Date('1800-01-01');
    var dataMaxima = new Date('2100-12-31');

    var dataValida = new Date(data);

    return dataValida >= dataMinima && dataValida <= dataMaxima;
}

function isValidCPF(cpf) {
    cpf = cpf.replace(/\D/g, '');

    if (cpf.length !== 11) {
        return false;
    }

    var digitosIguais = /^[0]{11}$/.test(cpf);
    if (digitosIguais) {
        return false;
    }

    var soma = 0;
    for (var i = 0; i < 9; i++) {
        soma += parseInt(cpf.charAt(i)) * (10 - i);
    }
    var resto = soma % 11;
    var digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

    if (parseInt(cpf.charAt(9)) !== digitoVerificador1) {
        return false;
    }

    soma = 0;
    for (var j = 0; j < 10; j++) {
        soma += parseInt(cpf.charAt(j)) * (11 - j);
    }
    resto = soma % 11;
    var digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

    if (parseInt(cpf.charAt(10)) !== digitoVerificador2) {
        return false;
    }

    return true;
}
