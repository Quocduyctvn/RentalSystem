

//===================================scroll top===============================================
var scroll = document.getElementById("scroll");
window.onscroll = function () {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        scroll.style.display = "block";
    } else {
        scroll.style.display = "none";
    }
}
function scroll_top() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}

//===================================Sort list Post ===============================================
const ItemsSort = document.querySelectorAll('.itemSort');

ItemsSort.forEach(item => {
    item.addEventListener('click', function () {
        ItemsSort.forEach(element => {
            element.classList.remove('text-underline');
            element.classList.remove('fw-bold');

        });
        this.classList.add('text-underline');
        this.classList.add('fw-bold');
    });
});

//===================================Menu ===============================================
const menuItems = document.querySelectorAll('.menuCategory');

menuItems.forEach(item => {
    item.addEventListener('click', function () {
        menuItems.forEach(element => {
            element.classList.remove('text-underline');
            element.classList.remove('text-black');

        });
        if (!this.classList.contains("trangchu")) {
            this.classList.add('text-underline');
            this.classList.add('text-black');
        }
    });
});

//===================================allow delete  many ===============================================
$(document).ready(function () {
    // Khi checkbox "chkAll" thay đổi trạng thái
    $("#chkAll").change(function () {
        var isChecked = $(this).prop('checked');
        // Chọn tất cả các checkbox trong trang
        $("td.js-col-checkbox > input").prop('checked', isChecked).change();
    });

    // Khi nhấp vào label của checkbox "chkAll"
    $("label[for='chkAll']").click(function () {
        // Kích hoạt sự kiện click của checkbox "chkAll"
        $("#chkAll").trigger("click");
    });

    // Khi checkbox trong mỗi hàng thay đổi trạng thái
    $("td.js-col-checkbox > input").change(function () {
        var isChecked = $(this).prop('checked');
        var delCountEle = $("#delCount");
        var delCountVal = Number(delCountEle.text());
        // Cập nhật số lượng các mục đã chọn
        if (isChecked) {
            delCountVal++;
        } else {
            delCountVal--;
        }
        delCountEle.text(delCountVal);

        // Kiểm tra nếu tất cả các checkbox đã được chọn
        $("#chkAll").prop('checked', delCountVal == $("td.js-col-checkbox > input").length);
    });

    // Khi checkbox "chkAllowBulkDel" thay đổi trạng thái
    $("#chkAllowBulkDel").change(function () {
        var isCheck = $(this).prop("checked");
        // Ẩn hoặc hiển thị các checkbox và nút xóa hàng loạt
        $(".js-col-checkbox").toggleClass("d-none", !isCheck);
        $("#btnBulkDel").toggleClass("d-none", !isCheck);
    });

    // Khi nhấp vào nút xóa hàng loạt
    $("#btnBulkDel").click(function () {
        var delCountEle = $("#delCount");
        var delCountVal = Number(delCountEle.text());
        if (delCountVal === 0) {
            alert("Chưa chọn mục để xóa");
            return;
        }
        var ids = $("td.js-col-checkbox > input:checked").map(function () {
            return $(this).data("id");
        }).get();

        // Lấy URL từ thuộc tính data-url của button
        var url = $("#btnBulkDel").data("url");

        $.ajax({
            url: url,
            type: "POST",
            data: { ids: ids },
            success: function () {
                // Xử lý sau khi xóa thành công nếu cần
                location.reload(); // Ví dụ: làm mới trang sau khi xóa
            },
            error: function () {
                // Xử lý khi có lỗi nếu cần
                alert("Đã xảy ra lỗi khi xóa hàng loạt");
            }
        });
    });

});
