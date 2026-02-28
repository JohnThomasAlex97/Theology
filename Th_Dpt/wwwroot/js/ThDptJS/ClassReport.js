$(document).ready(function () {

    loadData();
    loadClassDropdown();
    function loadClassDropdown() {
        $.ajax({
            url: '/Admin/GetClassList',
            type: 'GET',
            success: function (data) {
                var options = '<option value="">-- Select Class --</option>';
                $.each(data, function (i, item) {
                    options += `<option data-instructor="${item.instructor}" value="${item.className}">${item.className}</option>`;
                });
                $("#ClassName").html(options);
            }
        });
    }
    $("#ClassName").change(function () {
        var instructor = $("#ClassName option:selected").data("instructor");
        $("#Instructor").val(instructor);
    });
    function loadData() {
        $.get('/Report/GetAll', function (data) {

            var rows = '';
            $.each(data, function (i, item) {
                rows += `<tr>
                            <td>${item.id}</td>
                            <td>${item.className}</td>
                            <td>${item.instructor}</td>
                            <td>${item.attendanceCount}</td>
                            <td>${item.noBB}</td>
                            <td>${item.specialNotes ?? ''}</td>
                         </tr>`;
            });

            $('#classReportTable tbody').html(rows);
        });
    }

    $("#addForm").submit(function (e) {
        e.preventDefault();

        $.ajax({
            url: '/Report/Add',
            type: 'POST',
            data: $(this).serialize(),
            success: function (response) {
                if (response.success) {
                    $('#addModal').modal('hide');
                    loadData();
                    $('#addForm')[0].reset();
                }
            }
        });
    });

});