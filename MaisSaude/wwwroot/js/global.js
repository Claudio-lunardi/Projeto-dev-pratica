
$(document).ready(function () {

    $('.maskCelular').inputmask({ mask: ['(99) 99999-9999'] });
    $('.maskTelefone').inputmask({ mask: ['(99) 9999-9999'] });
    $('.maskCPF').inputmask({ mask: ['999.999.999-99'] });

    $('.maskCEP').inputmask({ mask: ['99999-999'] });

    $('#myTable').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Portuguese-Brasil.json"
        },
        "lengthMenu": [10, 15, 20, 30, 100]
    });

});

