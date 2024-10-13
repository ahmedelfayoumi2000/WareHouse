
<script>
    $(document).ready(function () {
        $('.carousel').carousel({
            interval: false
        });

    $('.carousel-control-prev').click(function () {
        $('.carousel').carousel('prev');
        });

    $('.carousel-control-next').click(function () {
        $('.carousel').carousel('next');
        });
    });
</script>

