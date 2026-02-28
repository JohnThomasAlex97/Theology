$(document).ready(function () {
    loadClassData();
    loadClassDropdown();
    $("#addForm").submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: '/Admin/AddEvangelism',
            type: 'POST',
            data: $(this).serialize(),
            success: function (response) {
                if (response.success) {
                    $("#addModal").modal('hide');
                    $("#addForm")[0].reset();
                    loadClassData();   // reload table
                } else {
                    alert("Error saving data.");
                }
            },
            error: function () {
                alert("Error occurred.");
            }
        });
    });
    $("#ClassName").change(function () {
        var instructor = $("#ClassName option:selected").data("instructor");
        $("#Instructor").val(instructor);
    });
});

function loadClassData() {
    $.ajax({
        url: '/Admin/GetEvangelismData',  // change controller name if needed
        type: 'GET',
        success: function (data) {
            var rows = "";
            if (data.length > 0) {
                $.each(data, function (index, item) {
                    rows += `<tr>
                                    <td>${item.id}</td>
                                    <td>${item.className}</td>
                                    <td>${item.instructor}</td>
                                    <td>${item.centerStudentName}</td>
                                    <td>${item.prospectName}</td>
                                    <td>${item.phoneNumber}</td>
                                    <td>${item.city}</td>
                                </tr>`;
                });
            } else {
                rows = `<tr>
                                <td colspan="7" class="text-danger">
                                    No Records Found
                                </td>
                            </tr>`;
            }
            $("#evangelismTable tbody").html(rows);
        },
        error: function () {
            alert("Error loading data.");
        }
    });
}
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