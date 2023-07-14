<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
        <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
        <script src="https://cdn.jsdelivr.net/npm/flatpickr/dist/l10n/uk.js"></script>


        <script>
            $(document).ready(function () {
                $("#deadline").datepicker();
    });
        </script>
        <script>
            document.addEventListener('DOMContentLoaded', function () {
                flatpickr("#deadline", {
                    enableTime: true,
                    dateFormat: "Y-m-d H:i",
                    locale: "uk"
                    // Інші опції, якщо потрібно
                });
    });
        </script>