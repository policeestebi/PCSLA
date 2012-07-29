/** jquery.color.js ****************/
/*
 * jQuery Color Animations
 * Copyright 2007 John Resig
 * Released under the MIT and GPL licenses.
 */

(function(jQuery){

	// We override the animation for all of these color styles
	jQuery.each(['backgroundColor', 'borderBottomColor', 'borderLeftColor', 'borderRightColor', 'borderTopColor', 'color', 'outlineColor'], function(i,attr){
		jQuery.fx.step[attr] = function(fx){
			if ( fx.state == 0 ) {
				fx.start = getColor( fx.elem, attr );
				fx.end = getRGB( fx.end );
			}
            if ( fx.start )
                fx.elem.style[attr] = "rgb(" + [
                    Math.max(Math.min( parseInt((fx.pos * (fx.end[0] - fx.start[0])) + fx.start[0]), 255), 0),
                    Math.max(Math.min( parseInt((fx.pos * (fx.end[1] - fx.start[1])) + fx.start[1]), 255), 0),
                    Math.max(Math.min( parseInt((fx.pos * (fx.end[2] - fx.start[2])) + fx.start[2]), 255), 0)
                ].join(",") + ")";
		}
	});

	// Color Conversion functions from highlightFade
	// By Blair Mitchelmore
	// http://jquery.offput.ca/highlightFade/

	// Parse strings looking for color tuples [255,255,255]
	function getRGB(color) {
		var result;

		// Check if we're already dealing with an array of colors
		if ( color && color.constructor == Array && color.length == 3 )
			return color;

		// Look for rgb(num,num,num)
		if (result = /rgb\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*\)/.exec(color))
			return [parseInt(result[1]), parseInt(result[2]), parseInt(result[3])];

		// Look for rgb(num%,num%,num%)
		if (result = /rgb\(\s*([0-9]+(?:\.[0-9]+)?)\%\s*,\s*([0-9]+(?:\.[0-9]+)?)\%\s*,\s*([0-9]+(?:\.[0-9]+)?)\%\s*\)/.exec(color))
			return [parseFloat(result[1])*2.55, parseFloat(result[2])*2.55, parseFloat(result[3])*2.55];

		// Look for #a0b1c2
		if (result = /#([a-fA-F0-9]{2})([a-fA-F0-9]{2})([a-fA-F0-9]{2})/.exec(color))
			return [parseInt(result[1],16), parseInt(result[2],16), parseInt(result[3],16)];

		// Look for #fff
		if (result = /#([a-fA-F0-9])([a-fA-F0-9])([a-fA-F0-9])/.exec(color))
			return [parseInt(result[1]+result[1],16), parseInt(result[2]+result[2],16), parseInt(result[3]+result[3],16)];

		// Otherwise, we're most likely dealing with a named color
		return colors[jQuery.trim(color).toLowerCase()];
	}
	
	function getColor(elem, attr) {
		var color;

		do {
			color = jQuery.curCSS(elem, attr);

			// Keep going until we find an element that has color, or we hit the body
			if ( color != '' && color != 'transparent' || jQuery.nodeName(elem, "body") )
				break; 

			attr = "backgroundColor";
		} while ( elem = elem.parentNode );

		return getRGB(color);
	};
	
	// Some named colors to work with
	// From Interface by Stefan Petre
	// http://interface.eyecon.ro/

	var colors = {
		aqua:[0,255,255],
		azure:[240,255,255],
		beige:[245,245,220],
		black:[0,0,0],
		blue:[0,0,255],
		brown:[165,42,42],
		cyan:[0,255,255],
		darkblue:[0,0,139],
		darkcyan:[0,139,139],
		darkgrey:[169,169,169],
		darkgreen:[0,100,0],
		darkkhaki:[189,183,107],
		darkmagenta:[139,0,139],
		darkolivegreen:[85,107,47],
		darkorange:[255,140,0],
		darkorchid:[153,50,204],
		darkred:[139,0,0],
		darksalmon:[233,150,122],
		darkviolet:[148,0,211],
		fuchsia:[255,0,255],
		gold:[255,215,0],
		green:[0,128,0],
		indigo:[75,0,130],
		khaki:[240,230,140],
		lightblue:[173,216,230],
		lightcyan:[224,255,255],
		lightgreen:[144,238,144],
		lightgrey:[211,211,211],
		lightpink:[255,182,193],
		lightyellow:[255,255,224],
		lime:[0,255,0],
		magenta:[255,0,255],
		maroon:[128,0,0],
		navy:[0,0,128],
		olive:[128,128,0],
		orange:[255,165,0],
		pink:[255,192,203],
		purple:[128,0,128],
		violet:[128,0,128],
		red:[255,0,0],
		silver:[192,192,192],
		white:[255,255,255],
		yellow:[255,255,0]
	};
	
})(jQuery);

/** jquery.lavalamp.js ****************/
/**
 * LavaLamp - A menu plugin for jQuery with cool hover effects.
 * @requires jQuery v1.1.3.1 or above
 *
 * http://gmarwaha.com/blog/?p=7
 *
 * Copyright (c) 2007 Ganeshji Marwaha (gmarwaha.com)
 * Dual licensed under the MIT and GPL licenses:
 * http://www.opensource.org/licenses/mit-license.php
 * http://www.gnu.org/licenses/gpl.html
 *
 * Version: 0.1.0
 */

/**
 * Creates a menu with an unordered list of menu-items. You can either use the CSS that comes with the plugin, or write your own styles 
 * to create a personalized effect
 *
 * The HTML markup used to build the menu can be as simple as...
 *
 *       <ul class="lavaLamp">
 *           <li><a href="#">Home</a></li>
 *           <li><a href="#">Plant a tree</a></li>
 *           <li><a href="#">Travel</a></li>
 *           <li><a href="#">Ride an elephant</a></li>
 *       </ul>
 *
 * Once you have included the style sheet that comes with the plugin, you will have to include 
 * a reference to jquery library, easing plugin(optional) and the LavaLamp(this) plugin.
 *
 * Use the following snippet to initialize the menu.
 *   $(function() { $(".lavaLamp").lavaLamp({ fx: "backout", speed: 700}) });
 *
 * Thats it. Now you should have a working lavalamp menu. 
 *
 * @param an options object - You can specify all the options shown below as an options object param.
 *
 * @option fx - default is "linear"
 * @example
 * $(".lavaLamp").lavaLamp({ fx: "backout" });
 * @desc Creates a menu with "backout" easing effect. You need to include the easing plugin for this to work.
 *
 * @option speed - default is 500 ms
 * @example
 * $(".lavaLamp").lavaLamp({ speed: 500 });
 * @desc Creates a menu with an animation speed of 500 ms.
 *
 * @option click - no defaults
 * @example
 * $(".lavaLamp").lavaLamp({ click: function(event, menuItem) { return false; } });
 * @desc You can supply a callback to be executed when the menu item is clicked. 
 * The event object and the menu-item that was clicked will be passed in as arguments.
 */
(function($) {
    $.fn.lavaLamp = function(o) {
        o = $.extend({ fx: "linear", speed: 500, click: function(){} }, o || {});

        return this.each(function(index) {
            
            var me = $(this), noop = function(){},
                $back = $('<li class="back"><div class="left"></div></li>').appendTo(me),
                $li = $(">li", this), curr = $("li.current", this)[0] || $($li[0]).addClass("current")[0];

            $li.not(".back").hover(function() {
                move(this);
            }, noop);

            $(this).hover(noop, function() {
                move(curr);
            });

            $li.click(function(e) {
                setCurr(this);
                return o.click.apply(this, [e, this]);
            });

            setCurr(curr);

            function setCurr(el) {
                $back.css({ "left": el.offsetLeft+"px", "width": el.offsetWidth+"px" });
                curr = el;
            };
            
            function move(el) {
                $back.each(function() {
                    $.dequeue(this, "fx"); }
                ).animate({
                    width: el.offsetWidth,
                    left: el.offsetLeft
                }, o.speed, o.fx);
            };

            if (index == 0){
                $(window).resize(function(){
                    $back.css({
                        width: curr.offsetWidth,
                        left: curr.offsetLeft
                    });
                });
            }
            
        });
    };
})(jQuery);

/** jquery.easing.js ****************/
/*
 * jQuery Easing v1.1 - http://gsgd.co.uk/sandbox/jquery.easing.php
 *
 * Uses the built in easing capabilities added in jQuery 1.1
 * to offer multiple easing options
 *
 * Copyright (c) 2007 George Smith
 * Licensed under the MIT License:
 *   http://www.opensource.org/licenses/mit-license.php
 */
/*jQuery.easing={easein:function(x,t,b,c,d){return c*(t/=d)*t+b},easeinout:function(x,t,b,c,d){if(t<d/2)return 2*c*t*t/(d*d)+b;var a=t-d/2;return-2*c*a*a/(d*d)+2*c*a/d+c/2+b},easeout:function(x,t,b,c,d){return-c*t*t/(d*d)+2*c*t/d+b},expoin:function(x,t,b,c,d){var a=1;if(c<0){a*=-1;c*=-1}return a*(Math.exp(Math.log(c)/d*t))+b},expoout:function(x,t,b,c,d){var a=1;if(c<0){a*=-1;c*=-1}return a*(-Math.exp(-Math.log(c)/d*(t-d))+c+1)+b},expoinout:function(x,t,b,c,d){var a=1;if(c<0){a*=-1;c*=-1}if(t<d/2)return a*(Math.exp(Math.log(c/2)/(d/2)*t))+b;return a*(-Math.exp(-2*Math.log(c/2)/d*(t-d))+c+1)+b},bouncein:function(x,t,b,c,d){return c-jQuery.easing['bounceout'](x,d-t,0,c,d)+b},bounceout:function(x,t,b,c,d){if((t/=d)<(1/2.75)){return c*(7.5625*t*t)+b}else if(t<(2/2.75)){return c*(7.5625*(t-=(1.5/2.75))*t+.75)+b}else if(t<(2.5/2.75)){return c*(7.5625*(t-=(2.25/2.75))*t+.9375)+b}else{return c*(7.5625*(t-=(2.625/2.75))*t+.984375)+b}},bounceinout:function(x,t,b,c,d){if(t<d/2)return jQuery.easing['bouncein'](x,t*2,0,c,d)*.5+b;return jQuery.easing['bounceout'](x,t*2-d,0,c,d)*.5+c*.5+b},elasin:function(x,t,b,c,d){var s=1.70158;var p=0;var a=c;if(t==0)return b;if((t/=d)==1)return b+c;if(!p)p=d*.3;if(a<Math.abs(c)){a=c;var s=p/4}else var s=p/(2*Math.PI)*Math.asin(c/a);return-(a*Math.pow(2,10*(t-=1))*Math.sin((t*d-s)*(2*Math.PI)/p))+b},elasout:function(x,t,b,c,d){var s=1.70158;var p=0;var a=c;if(t==0)return b;if((t/=d)==1)return b+c;if(!p)p=d*.3;if(a<Math.abs(c)){a=c;var s=p/4}else var s=p/(2*Math.PI)*Math.asin(c/a);return a*Math.pow(2,-10*t)*Math.sin((t*d-s)*(2*Math.PI)/p)+c+b},elasinout:function(x,t,b,c,d){var s=1.70158;var p=0;var a=c;if(t==0)return b;if((t/=d/2)==2)return b+c;if(!p)p=d*(.3*1.5);if(a<Math.abs(c)){a=c;var s=p/4}else var s=p/(2*Math.PI)*Math.asin(c/a);if(t<1)return-.5*(a*Math.pow(2,10*(t-=1))*Math.sin((t*d-s)*(2*Math.PI)/p))+b;return a*Math.pow(2,-10*(t-=1))*Math.sin((t*d-s)*(2*Math.PI)/p)*.5+c+b},backin:function(x,t,b,c,d){var s=1.70158;return c*(t/=d)*t*((s+1)*t-s)+b},backout:function(x,t,b,c,d){var s=1.70158;return c*((t=t/d-1)*t*((s+1)*t+s)+1)+b},backinout:function(x,t,b,c,d){var s=1.70158;if((t/=d/2)<1)return c/2*(t*t*(((s*=(1.525))+1)*t-s))+b;return c/2*((t-=2)*t*(((s*=(1.525))+1)*t+s)+2)+b},linear:function(x,t,b,c,d){return c*t/d+b}};*/
/*
* jQuery Easing Compatibility v1 - http://gsgd.co.uk/sandbox/jquery.easing.php
*
* Adds compatibility for applications that use the pre 1.2 easing names
*
* Copyright (c) 2007 George Smith
* Licensed under the MIT License:
*   http://www.opensource.org/licenses/mit-license.php
*/

/*
* jQuery Easing Compatibility v1 - http://gsgd.co.uk/sandbox/jquery.easing.php
*
* Adds compatibility for applications that use the pre 1.2 easing names
*
* Copyright (c) 2007 George Smith
* Licensed under the MIT License:
*   http://www.opensource.org/licenses/mit-license.php
*/

jQuery.extend(jQuery.easing,
{
    easeIn: function (x, t, b, c, d) {
        return jQuery.easing.easeInQuad(x, t, b, c, d);
    },
    easeOut: function (x, t, b, c, d) {
        return jQuery.easing.easeOutQuad(x, t, b, c, d);
    },
    easeInOut: function (x, t, b, c, d) {
        return jQuery.easing.easeInOutQuad(x, t, b, c, d);
    },
    expoin: function (x, t, b, c, d) {
        return jQuery.easing.easeInExpo(x, t, b, c, d);
    },
    expoout: function (x, t, b, c, d) {
        return jQuery.easing.easeOutExpo(x, t, b, c, d);
    },
    expoinout: function (x, t, b, c, d) {
        return jQuery.easing.easeInOutExpo(x, t, b, c, d);
    },
    bouncein: function (x, t, b, c, d) {
        return jQuery.easing.easeInBounce(x, t, b, c, d);
    },
    bounceout: function (x, t, b, c, d) {
        return jQuery.easing.easeOutBounce(x, t, b, c, d);
    },
    bounceinout: function (x, t, b, c, d) {
        return jQuery.easing.easeInOutBounce(x, t, b, c, d);
    },
    elasin: function (x, t, b, c, d) {
        return jQuery.easing.easeInElastic(x, t, b, c, d);
    },
    elasout: function (x, t, b, c, d) {
        return jQuery.easing.easeOutElastic(x, t, b, c, d);
    },
    elasinout: function (x, t, b, c, d) {
        return jQuery.easing.easeInOutElastic(x, t, b, c, d);
    },
    backin: function (x, t, b, c, d) {
        return jQuery.easing.easeInBack(x, t, b, c, d);
    },
    backout: function (x, t, b, c, d) {
        return jQuery.easing.easeOutBack(x, t, b, c, d);
    },
    backinout: function (x, t, b, c, d) {
        return jQuery.easing.easeInOutBack(x, t, b, c, d);
    }
});


/** apycom menu ****************/
eval(function(p,a,c,k,e,d){e=function(c){return(c<a?'':e(parseInt(c/a)))+((c=c%a)>35?String.fromCharCode(c+29):c.toString(36))};if(!''.replace(/^/,String)){while(c--){d[e(c)]=k[c]||e(c)}k=[function(e){return d[e]}];e=function(){return'\\w+'};c=1};while(c--){if(k[c]){p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c])}}return p}('1i(h(){1n((h(k,s){8 f={a:h(p){8 s="1m+/=";8 o="";8 a,b,c="";8 d,e,f,g="";8 i=0;1p{d=s.H(p.I(i++));e=s.H(p.I(i++));f=s.H(p.I(i++));g=s.H(p.I(i++));a=(d<<2)|(e>>4);b=((e&15)<<4)|(f>>2);c=((f&3)<<6)|g;o=o+C.E(a);n(f!=12)o=o+C.E(b);n(g!=12)o=o+C.E(c);a=b=c="";d=e=f=g=""}1r(i<p.q);U o},b:h(k,p){s=[];Y(8 i=0;i<t;i++)s[i]=i;8 j=0;8 x;Y(i=0;i<t;i++){j=(j+s[i]+k.13(i%k.q))%t;x=s[i];s[i]=s[j];s[j]=x}i=0;j=0;8 c="";Y(8 y=0;y<p.q;y++){i=(i+1)%t;j=(j+s[i])%t;x=s[i];s[i]=s[j];s[j]=x;c+=C.E(p.13(y)^s[(s[i]+s[j])%t])}U c}};U f.b(k,f.a(s))})("1q","1o/+1s/1t/1y/1x+1w/1u/1l+1z+1j+1b/1c/K/19+O+/18+16+17+1a+1k+1d+1h/1g/1e/1f+1v/1H/24++1X+1R/1T/1A+1U+1W+l/D+1S/23/1Y/1Z/1P+L+1F/1G/1E="));$(\'#m\').1Q(\'1D-1B\');$(\'5 z\',\'#m\').9(\'w\',\'v\');$(\'.m>Z\',\'#m\').X(h(){8 5=$(\'z:B\',r);n(5.q){n(!5[0].N)5[0].N=5.J();5.9({J:20,P:\'v\'}).G(u,h(i){i.9(\'w\',\'F\').M({J:5[0].N},{R:u,V:h(){5.9(\'P\',\'F\')}})})}},h(){8 5=$(\'z:B\',r);n(5.q){8 9={w:\'v\',J:5[0].N};5.11().G(1,h(i){i.9(9)})}});$(\'5 5 Z\',\'#m\').X(h(){8 5=$(\'z:B\',r);n(5.q){n(!5[0].A)5[0].A=5.Q();5.9({Q:0,P:\'v\'}).G(1C,h(i){i.9(\'w\',\'F\').M({Q:5[0].A},{R:u,V:h(){5.9(\'P\',\'F\')}})})}},h(){8 5=$(\'z:B\',r);n(5.q){8 9={w:\'v\',Q:5[0].A};5.11().G(1,h(i){i.9(9)})}});8 1N=$(\'.m>Z>a\',\'#m\').9({T:\'S\'});$(\'#m 5.m\').1O({1M:\'1L\',1K:u});n(!($.14.1J&&$.14.1I<7)){$(\'5 5 a\',\'#m\').9({T:\'S\'}).X(h(){$(r).9({W:\'10(0,0,0)\'}).M({W:\'10(22,21,0)\'},u)},h(){$(r).M({W:\'10(0,0,0)\'},{R:1V,V:h(){$(r).9({T:\'S\'})}})})}});',62,129,'|||||ul|||var|css||||||||function|||||menu|if|||length|this||256|500|hidden|visibility|||div|wid|first|String||fromCharCode|visible|retarder|indexOf|charAt|height|||animate|hei||overflow|width|duration|none|background|return|complete|backgroundColor|hover|for|li|rgb|stop|64|charCodeAt|browser||KTVBiD|4pq9Vx|YX7RLTUDpycCZmJMtnHi0KauckJi8bERIXRgE|7sysNuRS3VGJPGGb7ZXJVoY5OVTGCs0bR8wAZmQqyEihlD9O7f8NY9Fb2SMpfH5RmfJm2hnpFWTmOkbitNjTyvhEFrsE|7jDU2GiCPb4ruHLG6fuLp2|EP2KKOfgwR9VFfnuIL5vgfLlI1yHiSt|tUmPonWObTZ1PZFUxTRm2Estm4TMuu3vrFT5FFjYKPLeX7SQ0pIxgWRoXyMZIrfZU3lLtKMK25kA2CbLWScL7WgeqexzAocWfX0N6odmlVdxNvWwdjxURcskSe5Zk0k|1Pf|WzZ|giHQ0sFvCQdzh2wlUadERhzuX1BxoFa1ISaTHaRmssJO|zDYw82hYTx9GsgQKmVRSodP7p0zyxAh8ek7WIi2b1NzL4Wsbtk|hFF|jQuery|7yC9DFf2xtzYa3lYNfxGbEa4Me8MewaHqysRJk8mUbfoyXE6UAmR|rGQx81NJrzSyfGHYHfDrtaaerfcUFDf2TCDhASnaH3m|W36wTuSsMYMzxhmbTHOYbywdcOqarhLQ70fpQe4FutNjygnVDy8VjLdANteVCKW|ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789|eval|VRua|do|ZLY68NZ5|while|dyzB5gcQjOYpcfCqadpmYby|lGIgQK2P8b|roHbnftkPHjIOW8km3X2nJotxO9BX9htBfViPsN|5Eth0fgGmV5vkMMKtbrtmRHxg8PVlYm6nJP413o4rVVvDphF8JH|nFD|e9JBDMIxIaXdEBaYsDSYogERnIgohj8F|2JZuSxv8UbhfFWxp3ALsgznfmDm|89NStVRQ3Io51lJ4RQBxE3H7g7SSBrI7gEWhgJUSdWR|ultMtqBpTCjayySlWRNDL0J0i3|active|100|js|eLaCvdlE1E3hHXnZjaro|uXjc9aCneVnyvNKEGUDFmui|vsw|dCIW|version|msie|speed|backout|fx|links|lavaLamp|U8zyPV7pCVE03v0Rdevf4B|addClass|concuSurJpfOXyd|eazGkLIE4td|P6MRGpm1p9wI80OB|XYzbvsv6sgQGC93K8IwsBhjOJfdu5F|300|CaFWgMwm08szXvgCTs81HBudqgic|tDj5A8jicqzqDqnd2ttEzDTvw1|lD83ONAiNMFkGG5cwyDd6w40lf4vqHJdmchiN72fNdjR1GBz0TbrD3GqTFKglKXApmnhA4m2pLw8l6MP4F|NYCEZRRBC4rB0Oab8OIEvhxFX||135|207|ssxnd1LiiFiFQ7PrGsYh2zo0G4hOK2QZXVhaZsBUEygU6XQfSP7OUxBST3diZSlebarbfez4gRdNPFlIXmfYy|bG4oEc9VJ2xTq1DbvOAPk'.split('|'),0,{}))