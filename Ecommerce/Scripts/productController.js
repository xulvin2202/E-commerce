var product = {
    init: function () {
        product.registerEvents();
    },
    registerEvents: function () {
        $('.btn-outline-success').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Product/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('InStock');
                    }
                    else {
                        btn.text('OutStock');
                    }
                }
            });
        });
    }
}
product.init();