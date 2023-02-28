function mapPartialView(className, url, method, fields) {
    $('.' + className).click(function (e) {
        e.preventDefault();

        const data = {};
        for (let i = 0; i < fields.length; i++) {
            data[fields[i]] = $(this).data(fields[i]);
        }
        
        $.ajax({
            url: url,
            type: method,
            data: data,
            success: function (result) {
                $("main").html(result);
            }
        });
    })
}