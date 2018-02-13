jQuery(document).ready(function() {
	
	new jBox('Tooltip', {
		attach: jQuery('#himachal'),
		theme: 'TooltipBorder',
	   content: '<div class="linking"><ul> <li><a href="#">Update Author Contract</a></li> <li><a href="#">Request For Sub-Licensing Contract</a></li> <li><a href="#">Update Sap Agreement No</a></li>  <li><a href="#">Request For Rights Contract</a></li>  <li><a href="#">Create In-bound Permission</a></li> <li><a href="#">Out-bound Permission</a></li></ul>  </div>	',
		animation: 'move',
	
		closeOnMouseleave: true,
		closeButton: 'box'
	});
	
	
		
});