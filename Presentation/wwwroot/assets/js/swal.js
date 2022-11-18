var SWAL = {
    init: function () {
        SWAL.alerts();
        SWAL.confirms();
        SWAL.confirmCallBack();
    },
    alerts: function (param) {
        swal({
            title: param,
            confirmButtonText: "Tamam"
        });
    },
    callBackAlert: function (func, paramx) {
        swal({
            title: paramx
        },
        function () {
            func();
        });
    },
    confirms: function (func, param, param1, param2) {
        if (typeof(param1)==='undefined') param1 = "Kaydet";
        if (typeof(param2)==='undefined') param2 = "Hayır";
        swal({
            title: param,
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: param1,
            cancelButtonText: param2,
            closeOnConfirm: false
        }, function () {
            func();
            $(".modal").modal("hide");
        });

    },
}
