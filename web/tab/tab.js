/**
 * @author stone
 * @github https://github.com/stone0090/code-hodgepodge/tree/master/web/tab
 * @version 1.0.0
 * ===================================================
 * opts.wrap 	tab外围容器/滑动事件对象(id选择器)
 * opts.menu 	tab菜单容器/点击事件对象(id选择器)
 * opts.content	tab内容容器/滑动切换对象(id选择器) 
 * opts.index 	tab索引(默认0)，指定显示哪个索引的菜单、内容
 * opts.currentClassName    tab当前项的类名(默认为current)
 * opts.useDefualtCSS   是否使用默认样式(默认为true，需引入tab.css)
 * opts.duration    滑动的速度(默认为300，单位毫秒)
 * opts.callback    点击滑动时的回调函数(和菜单数量一致)
 * ===================================================
 **/
;
(function(window, document, Math, undefined) {
    'use strict';

    var rAF = window.requestAnimationFrame ||
        window.webkitRequestAnimationFrame ||
        window.mozRequestAnimationFrame ||
        window.oRequestAnimationFrame ||
        window.msRequestAnimationFrame ||
        function(callback) {
            window.setTimeout(callback, 1000 / 60);
        };

    var utils = (function() {
        var me = {};

        var _elementStyle = document.createElement('div').style;
        var _vendor = (function() {
            var vendors = ['t', 'webkitT', 'MozT', 'msT', 'OT'],
                transform,
                l = vendors.length;

            for (var i = 0; i < l; i++) {
                transform = vendors[i] + 'ransform';
                if (transform in _elementStyle) {
                    return vendors[i].substr(0, vendors[i].length - 1);
                }
            }

            return false;
        })();

        me.prefixStyle = function(style) {
            if (_vendor === false) return false;
            if (_vendor === '') return style;
            return _vendor + style.charAt(0).toUpperCase() + style.substr(1);
        };
        me.prefixHandler = function(handler) {
            if (_vendor === false) return false;
            if (_vendor === '') return handler;
            return _vendor.replace('ms', '') + handler.charAt(0).toUpperCase() + handler.substr(1);
        };

        me.getTime = Date.now || function getTime() {
            return new Date().getTime();
        };

        me.extend = function(target, obj) {
            for (var i in obj) {
                target[i] = obj[i];
            }
        };

        me.addHandler = function(el, type, handler, args) {
            if (el.addEventListener) {
                el.addEventListener(type, handler, false);
            } else if (el.attachEvent) {
                el.attachEvent('on' + type, handler);
            } else {
                el['on' + type] = handler;
            }
        };
        me.removeHandler = function(el, type, handler, args) {
            if (el.removeEventListener) {
                el.removeEventListener(type, handler, false);
            } else if (el.detachEvent) {
                el.detachEvent('on' + type, handler);
            } else {
                el['on' + type] = null;
            }
        };

        me.prefixPointerEvent = function(pointerEvent) {
            return window.MSPointerEvent ?
                'MSPointer' + pointerEvent.charAt(7).toUpperCase() + pointerEvent.substr(8) :
                pointerEvent;
        };

        me.extend(me, {
            hasTransform: me.prefixStyle('transform') in _elementStyle,
            hasTransition: me.prefixStyle('transition') in _elementStyle,
            hasPerspective: me.prefixStyle('perspective') in _elementStyle,
            hasTouch: 'ontouchstart' in window,
            hasPointer: !!(window.PointerEvent || window.MSPointerEvent) // IE10 is prefixed
        });

        me.extend(me.style = {}, {
            transform: me.prefixStyle('transform'),
            transitionTimingFunction: me.prefixStyle('transitionTimingFunction'),
            transitionDuration: me.prefixStyle('transitionDuration'),
            transitionDelay: me.prefixStyle('transitionDelay'),
            transformOrigin: me.prefixStyle('transformOrigin')
        });

        me.extend(me.eventType = {}, {
            touchstart: 1,
            touchmove: 1,
            touchend: 1,

            mousedown: 2,
            mousemove: 2,
            mouseup: 2,

            pointerdown: 3,
            pointermove: 3,
            pointerup: 3,

            MSPointerDown: 3,
            MSPointerMove: 3,
            MSPointerUp: 3
        });

        me.hasClass = function(e, c) {
            var re = new RegExp("(^|\\s)" + c + "(\\s|$)");
            return re.test(e.className);
        };

        me.addClass = function(e, c) {
            if (me.hasClass(e, c)) {
                return;
            }
            var newclass = e.className.split(' ');
            newclass.push(c);
            e.className = newclass.join(' ');
        };

        me.removeClass = function(e, c) {
            if (!me.hasClass(e, c)) {
                return;
            }
            var re = new RegExp("(^|\\s)" + c + "(\\s|$)", 'g');
            e.className = e.className.replace(re, ' ');
        };

        me.ready = function(callback) {
            if (/complete|loaded|interactive/.test(document.readyState) && document.body) {
                callback();
            } else {
                if (document.addEventListener) {
                    document.addEventListener('DOMContentLoaded', callback, false);
                } else if (document.attachEvent) {
                    document.attachEvent('onreadystatechange', callback);
                } else {
                    document.onreadystatechange = callback;
                }
            }
        };

        return me;
    })();

    // ie6 ~ ie8 not support trim()
    String.prototype.trim = function() {
        return this.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
    };

    var Tab = function(opts) {
        if (typeof opts === undefined) {
            return;
        }

        this.wrap = document.getElementById(opts.wrap);
        this.menu = document.getElementById(opts.menu);
        this.content = document.getElementById(opts.content);

        this.menus = this.menu.children;
        this.contents = this.content.children;

        this.length = this.menus.length;
        if (this.length < 1) {
            return;
        }

        this.touch = {};
        this._events = {};

        this.opts = {
            currentClassName: 'current',
            useDefualtCSS: true,
            duration: 300,
            callback: []
        };

        for (var i in opts) {
            this.opts[i] = opts[i];
        }

        if (this.opts.index > this.length - 1) {
            this.opts.index = this.length - 1;
        }
        this.index = this.oldIndex = this.opts.index || 0;

        this._init();
    };

    Tab.prototype = {
        _init: function() {
            this._initSize();
            this._initClass();
            this._initEvent();
        },

        destroy: function() {
            // TODO
            // this._initEvents(true);
            // clearTimeout(this.resizeTimeout);
            // this.resizeTimeout = null;
            // this._execEvent('destroy');
        },

        _initSize: function() {
            if (this.opts.useDefualtCSS) {
                this.height = document.documentElement.clientHeight || document.body.clientHeight;
                this.content.style.height = (this.height - 45) + 'px';
            }
            this.width = document.documentElement.clientWidth || document.body.clientWidth;
            this.content.style.width = this.length * this.width + 'px';
            for (var i = 0; i < this.length; i++) {
                this.contents[i].style.width = this.width + 'px';
            }
            // this._translate();
        },
        _resize: function() {
            var me = this;
            window.setTimeout(function() {
                me._initSize();
            }, 100);
        },
        _initClass: function() {
            if (this.opts.useDefualtCSS) {
                this.content.className += ' tab-content';
                this.menu.className += ' tab-menu';
            }
            for (var i = 0; i < this.length; i++) {
                this.menus[i].index = i;
                this.menus[i].className = this.menus[i].className.replace(this.opts.currentClassName, '');
                this.contents[i].className = this.contents[i].className.replace(this.opts.currentClassName, '');
            }
            this.menus[this.index].className += ' ' + this.opts.currentClassName;
            this.contents[this.index].className += ' ' + this.opts.currentClassName;
        },
        _initEvent: function(remove) {
            var handler = remove ? utils.removeHandler : utils.addHandler;
            var me = this;

            handler(window, 'resize', function(e) {
                me._resize(e);
            });
            handler(window, 'orientationchange', function(e) {
                me._resize(e);
            });

            if (utils.hasTouch) {
                handler(this.wrap, 'touchstart', function(e) {
                    me._touchStart(e);
                });
                handler(this.wrap, 'touchmove', function(e) {
                    me._touchMove(e);
                });
                handler(this.wrap, 'touchcancel', function(e) {
                    me._touchEnd(e);
                });
                handler(this.wrap, 'touchend', function(e) {
                    me._touchEnd(e);
                });
            }

            if (utils.hasPointer) {
                handler(this.wrap, utils.prefixPointerEvent('pointerdown'), function(e) {
                    me._touchStart(e);
                });
                handler(this.wrap, utils.prefixPointerEvent('pointermove'), function(e) {
                    me._touchMove(e);
                });
                handler(this.wrap, utils.prefixPointerEvent('pointercancel'), function(e) {
                    me._touchEnd(e);
                });
                handler(this.wrap, utils.prefixPointerEvent('pointerup'), function(e) {
                    me._touchEnd(e);
                });
            }

            handler(this.wrap, 'mousedown', function(e) {
                me._touchStart(e);
            });
            handler(this.wrap, 'mousemove', function(e) {
                me._touchMove(e);
            });
            handler(this.wrap, 'mousecancel', function(e) {
                me._touchEnd(e);
            });
            handler(this.wrap, 'mouseup', function(e) {
                me._touchEnd(e);
            });

            handler(this.menu, 'click', function(e) {
                me._touchClick(e);
            });
            handler(this.menu, 'touchend', function(e) {
                me._touchClick(e);
            });
            handler(this.content, utils.prefixHandler('transitionEnd'), function(e) {
                me._transitionEnd(e);
            });
        },
        // // ie6 ~ ie8 not support  
        // _initEvent: function(remove) {
        //     var handler = remove ? utils.removeHandler : utils.addHandler;

        //     handler(window, 'resize', this);
        //     handler(window, 'orientationchange', this);

        //     if (utils.hasTouch) {
        //         handler(this.wrap, 'touchstart', this);
        //         handler(this.wrap, 'touchmove', this);
        //         handler(this.wrap, 'touchcancel', this);
        //         handler(this.wrap, 'touchend', this);
        //     }

        //     if (utils.hasPointer) {
        //         handler(this.wrap, utils.prefixPointerEvent('pointerdown'), this);
        //         handler(this.wrap, utils.prefixPointerEvent('pointermove'), this);
        //         handler(this.wrap, utils.prefixPointerEvent('pointercancel'), this);
        //         handler(this.wrap, utils.prefixPointerEvent('pointerup'), this);
        //     }

        //     handler(this.wrap, 'mousedown', this);
        //     handler(this.wrap, 'mousemove', this);
        //     handler(this.wrap, 'mousecancel', this);
        //     handler(this.wrap, 'mouseup', this);

        //     handler(this.menu, 'click', this);
        //     handler(this.content, utils.prefixHandler('transitionEnd'), this);
        // },
        // handleEvent: function(e) {
        //     switch (e.type) {
        //         case 'touchstart':
        //         case 'pointerdown':
        //         case 'MSPointerDown':
        //         case 'mousedown':
        //             this._touchStart(e);
        //             break;
        //         case 'touchmove':
        //         case 'pointermove':
        //         case 'MSPointerMove':
        //         case 'mousemove':
        //             this._touchMove(e);
        //             break;
        //         case 'touchend':
        //         case 'pointerup':
        //         case 'MSPointerUp':
        //         case 'mouseup':
        //         case 'touchcancel':
        //         case 'pointercancel':
        //         case 'MSPointerCancel':
        //         case 'mousecancel':
        //             this._touchEnd(e);
        //             break;
        //         case 'transitionend':
        //         case 'webkitTransitionEnd':
        //         case 'oTransitionEnd':
        //         case 'MSTransitionEnd':
        //             this._transitionEnd(e);
        //             break;
        //         case 'click':
        //             this._touchClick(e);
        //             break;
        //         case 'orientationchange':
        //         case 'resize':
        //             this._resize();
        //             break;
        //     }
        // },
        _touchStart: function(e) {
            // React to left mouse button only
            if (utils.eventType[e.type] != 1) {
                // for button property
                // http://unixpapa.com/js/mouse.html
                var button;
                if (!e.which) {
                    button = (e.button < 2) ? 0 : ((e.button == 4) ? 1 : 2); // IE case 
                } else {
                    button = e.button; // All others
                }
                if (button !== 0) {
                    return;
                }
            }

            if (this.initiated && utils.eventType[e.type] !== this.initiated) {
                return;
            }

            // ie6 ~ ie8 not support e.preventDefault
            if (e.preventDefault) {
                e.preventDefault();
            } else {
                e.returnValue = false;
            }

            this.initiated = utils.eventType[e.type];

            // ie6 ~ ie8 not support e.pageX
            var point = e.touches ? e.touches[0] : e;
            var pageX = (point.pageX) ? point.pageX :
                e.clientX + (document.documentElement.scrollLeft ?
                    document.documentElement.scrollLeft :
                    document.body.scrollLeft);
            var pageY = (point.pageY) ? point.pageY :
                e.clientY + (document.documentElement.scrollTop ?
                    document.documentElement.scrollTop :
                    document.body.scrollTop);

            this.touch.x = pageX;
            this.touch.y = pageY;
            this.touch.time = Date.now();
            this.touch.disX = 0;
            this.touch.disY = 0;
            this.touch.fixed = '';
        },
        _touchMove: function(e) {
            if (utils.eventType[e.type] !== this.initiated) {
                return;
            }

            if (this.touch.fixed === 'up') {
                this.initiated = false;
                return;
            }

            // ie6 ~ ie8 not support e.preventDefault
            if (e.preventDefault) {
                e.preventDefault();
            } else {
                e.returnValue = false;
            }

            // ie6 ~ ie8 not support e.pageX
            var point = e.touches ? e.touches[0] : e;
            var pageX = (point.pageX) ? point.pageX :
                e.clientX + (document.documentElement.scrollLeft ?
                    document.documentElement.scrollLeft :
                    document.body.scrollLeft);
            var pageY = (point.pageY) ? point.pageY :
                e.clientY + (document.documentElement.scrollTop ?
                    document.documentElement.scrollTop :
                    document.body.scrollTop);

            if (point.length > 1 || e.scale && e.scale !== 1) {
                return;
            }

            this.touch.disX = pageX - this.touch.x;
            this.touch.disY = pageY - this.touch.y;
            if (this.touch.fixed === '') {
                if (Math.abs(this.touch.disY) > Math.abs(this.touch.disX)) {
                    this.touch.fixed = 'up';
                } else {
                    this.touch.fixed = 'left';
                }
            }
            if (this.touch.fixed === 'left') {
                if ((this.index === 0 && this.touch.disX > 0) || (this.index === this.length - 1 && this.touch.disX < 0)) {
                    this.touch.disX /= 4;
                }

                this._translate(this.touch.disX - this.index * this.width, true);
            }
        },
        _touchEnd: function(e) {

            if (!this.initiated) {
                return;
            }

            this.initiated = false;

            // ie6 ~ ie8 not support e.preventDefault
            if (e.preventDefault) {
                e.preventDefault();
            } else {
                e.returnValue = false;
            }

            // ie6 ~ ie8 not support e.stopPropagation
            if (e.stopPropagation) {
                e.stopPropagation();
            } else {
                e.cancelBubble = false;
            }

            if (this.touch.fixed === 'left') {
                var absX = Math.abs(this.touch.disX);
                if ((Date.now() - this.touch.time > 100 && absX > 10) || absX > this.width / 2) {
                    this.touch.time = Date.now();
                    if (this.touch.disX > 0) {
                        this.index--;
                    } else {
                        this.index++;
                    }
                    if (this.index < 0) {
                        this.index = 0;
                    }
                    if (this.index > this.length - 1) {
                        this.index = this.length - 1;
                    }
                    if (this.index !== this.oldIndex) {
                        this._replace();
                    }
                }

                this._translate();

                if (this.opts.callback[this.index]) {
                    this.opts.callback[this.index]();
                }
            }
        },
        _transitionEnd: function() {
            this._execEvent('scrollEnd');
        },
        _touchClick: function(e) {
            var target = e.target || e.srcElement;
            if (target.nodeType === 1 && typeof target.index !== undefined) {
                if (target.index === this.index) {
                    return;
                }

                // ie6 ~ ie8 not support e.preventDefault
                if (e.preventDefault) {
                    e.preventDefault();
                } else {
                    e.returnValue = false;
                }

                // ie6 ~ ie8 not support e.stopPropagation
                if (e.stopPropagation) {
                    e.stopPropagation();
                } else {
                    e.cancelBubble = false;
                }

                this.index = target.index;
                this._translate();

                if (this.opts.callback[this.index]) {
                    this.opts.callback[this.index]();
                }
                this._replace();
            }
        },
        _replace: function() {
            this.menus[this.index].className += ' ' + this.opts.currentClassName;
            this.menus[this.oldIndex].className = this.menus[this.oldIndex].className.replace(this.opts.currentClassName, '').trim();
            this.contents[this.index].className += ' ' + this.opts.currentClassName;
            this.contents[this.oldIndex].className = this.contents[this.oldIndex].className.replace(this.opts.currentClassName, '').trim();
            this.oldIndex = this.index;
        },
        _translate: function(moveX) {
            var destX = moveX || (-this.index * this.width);
            if (utils.hasTransform && utils.hasTransition) {
                this.content.style[utils.prefixStyle('transform')] = 'translateX(' + destX + 'px)';
                this.content.style[utils.prefixStyle('transition')] = 'all ' + this.opts.duration + 'ms';
            } else {
                this.content.style.left = destX + 'px';
                if (moveX) {
                    this.x = moveX;
                } else {
                    this._updatePosition();
                }
            }
        },
        _updatePosition: function() {
            var me = this,
                startX = me.x || 0,
                destX = -this.index * this.width,
                duration = this.opts.duration,
                startTime = utils.getTime(),
                destTime = startTime + duration;

            function step() {
                var now = utils.getTime(),
                    newX, newY;

                if (now >= destTime || (startX === destX)) {
                    me.isAnimating = false;
                    me.x = destX;
                    me.content.style.left = destX + 'px';
                    return;
                }

                now = (now - startTime) / duration;
                newX = (destX - startX) * now + startX;
                me.content.style.left = newX + 'px';

                if (me.isAnimating) {
                    rAF(step);
                }
            }

            me.isAnimating = true;
            step();
        },
        _execEvent: function(type) {
            if (!this._events[type]) {
                return;
            }
            var i = 0,
                l = this._events[type].length;

            if (!l) {
                return;
            }

            for (; i < l; i++) {
                this._events[type][i].apply(this, [].slice.call(arguments, 1));
            }
        }
    };

    window.Tab = Tab;

})(window, document, Math);