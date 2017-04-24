$(document).ready(function() {
	$('#myCarousel').carousel({
	interval: 100
	})
    
    $('#myCarousel').on('slid.bs.carousel', function() {
    	//alert("slid");
	});
    
    
});

