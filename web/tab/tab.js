/**
 * @author stone
 * @github https://github.com/stone0090/code-hodgepodge/tree/master/web/tab
 * @version 1.0.0
 * ===================================================
 * opts.wrap    tab外围容器/滑动事件对象(id选择器)
 * opts.menu    tab菜单容器/点击事件对象(id选择器)
 * opts.content tab内容容器/滑动切换对象(id选择器) 
 * opts.index   指定首次显示的菜单及内容(默认为0)
 * opts.duration    滑动的速度(默认为300，单位毫秒)
 * opts.callback    点击菜单或滑动时的回调(需和菜单数量一致)
 * opts.selectedClass   被选中菜单的类名(默认为'current')
 * opts.useDefualtCSS   是否使用默认样式(默认为false) 
 * ===================================================
 **/
;
(function (window, document, undefined) {
    'use strict';

    var utils = (function () {
        var me = {};

        var _elementStyle = document.createElement('div').style;
        var _vendor = (function () {
            var vendors = ['t', 'webkitT', 'mozT', 'msT', 'oT'],
                transform,
                l = vendors.length;
            for (var i = 0; i < l; i++) {
                transform = vendors[i] + 'ransform';
                if (transform in _elementStyle)
                    return vendors[i].substr(0, vendors[i].length - 1);
            }
            return false;
        })();

        me.prefixStyle = function (style) {
            if (_vendor === false) return false;
            if (_vendor === '') return style;
            return _vendor + style.charAt(0).toUpperCase() + style.substr(1);
        };
        me.prefixHandler = function (handler) {
            if (_vendor === false) return false;
            if (_vendor === '') return handler;
            return _vendor.replace('ms', '') + handler.charAt(0).toUpperCase() + handler.substr(1);
        };

        me.getTime = Date.now || function getTime() {
            return new Date().getTime();
        };

        me.extend = function (target, obj) {
            for (var i in obj) {
                target[i] = obj[i];
            }
        };

        me.addHandler = function (el, type, handler, args) {
            if (el.addEventListener) {
                el.addEventListener(type, handler, false);
            } else if (el.attachEvent) {
                el.attachEvent('on' + type, handler);
            } else {
                el['on' + type] = handler;
            }
        };
        me.removeHandler = function (el, type, handler, args) {
            if (el.removeEventListener) {
                el.removeEventListener(type, handler, false);
            } else if (el.detachEvent) {
                el.detachEvent('on' + type, handler);
            } else {
                el['on' + type] = null;
            }
        };

        me.prefixPointerEvent = function (pointerEvent) {
            return window.MSPointerEvent ? 'MSPointer' + pointerEvent.charAt(7).toUpperCase() + pointerEvent.substr(8) : pointerEvent;
        };

        me.extend(me, {
            hasTransform: me.prefixStyle('transform') in _elementStyle,
            hasTransition: me.prefixStyle('transition') in _elementStyle,
            hasPerspective: me.prefixStyle('perspective') in _elementStyle,
            hasTouch: 'ontouchstart' in window,
            hasPointer: !!(window.PointerEvent || window.MSPointerEvent)
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

        me.hasClass = function (e, c) {
            var re = new RegExp("(^|\\s)" + c + "(\\s|$)");
            return re.test(e.className);
        };

        me.addClass = function (e, c) {
            if (me.hasClass(e, c))
                return;
            var newclass = e.className.split(' ');
            newclass.push(c);
            e.className = newclass.join(' ');
        };

        me.removeClass = function (e, c) {
            if (!me.hasClass(e, c))
                return;
            var re = new RegExp("(^|\\s)" + c + "(\\s|$)", 'g');
            e.className = e.className.replace(re, ' ');
        };

        me.ready = function (callback) {
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

        me.event = {};
        me.event.preventDefault = function (e) {
            // ie6 ~ ie8 not support e.preventDefault
            if (e.preventDefault) {
                e.preventDefault();
            } else {
                e.returnValue = false;
            }
        };
        me.event.stopPropagation = function (e) {
            // ie6 ~ ie8 not support e.stopPropagation
            if (e.stopPropagation) {
                e.stopPropagation();
            } else {
                e.cancelBubble = false;
            }
        };

        me.event.pageX = function (e) {
            // ie6 ~ ie8 not support e.pageX
            var point = e.touches ? e.touches[0] : e;
            var pageX = (point.pageX) ? point.pageX :
                e.clientX + (document.documentElement.scrollLeft ?
                    document.documentElement.scrollLeft :
                    document.body.scrollLeft);
            return pageX;
        };

        me.event.pageY = function (e) {
            // ie6 ~ ie8 not support e.pageY
            var point = e.touches ? e.touches[0] : e;
            var pageY = (point.pageY) ? point.pageY :
                e.clientY + (document.documentElement.scrollTop ?
                    document.documentElement.scrollTop :
                    document.body.scrollTop);
            return pageY;
        };

        me.log = function (msg) {
            if (console && console.log) {
                console.log(msg);
            }
        };

        me.rAF = window.requestAnimationFrame ||
            window.webkitRequestAnimationFrame ||
            window.mozRequestAnimationFrame ||
            window.oRequestAnimationFrame ||
            window.msRequestAnimationFrame ||
            function (callback) {
                window.setTimeout(callback, 1000 / 60);
            };

        return me;
    })();

    var Tab = function (opts) {
        if (opts === undefined || opts === null) {
            return;
        }

        this.wrap = document.getElementById(opts.wrap);
        this.menu = document.getElementById(opts.menu);
        this.content = document.getElementById(opts.content);
        if (!this.wrap || !this.menu || !this.content) {
            return;
        }

        this.menus = this.menu.children;
        this.contents = this.content.children;
        this.length = this.menus.length;
        if (this.length < 1) {
            return;
        }

        this.defualtOpts = {
            index: 0,
            duration: 300,
            callback: [],
            selectedClass: 'current',
            useDefualtCSS: false
        };

        for (var opt in this.defualtOpts) {
            if (this.defualtOpts.hasOwnProperty(opt)) {
                this[opt] = opts[opt] || this.defualtOpts[opt];
            }
        }

        if (this.defualtOpts.index > this.length - 1)
            this.defualtOpts.index = this.length - 1;
        if (this.defualtOpts.index < 0)
            this.defualtOpts.index = 0;
        this.index = this.oldIndex = this.defualtOpts.index || 0;

        this.touch = {};
        this.events = {};

        this._init();
    };

    Tab.prototype = {

        _init: function () {
            this._initClass();
            this._initSize();
            this._initEvent();
        },

        _initClass: function () {
            if (this.useDefualtCSS) {
                var width = 100 / this.length;
                document.write('<style>' +
                    'html, body { margin: 0; padding: 0; overflow: hidden; } \r\n' +
                    '.tab-content { position: relative; } \r\n' +
                    '.tab-content > div { float: left; } \r\n' +
                    '.tab-menu { position: absolute; bottom: 0; border-top: 2px solid #9ac7ed; width: 100%; height: 45px; } \r\n' +
                    '.tab-menu > div { float: left; width: ' + width + '%; height: 100%; color: #2a70be; line-height: 45px; text-align: center; background: #ECF2F6; border-left: 1px solid #E5E6E7; margin-right: -1px; } \r\n' +
                    '.tab-menu > .current { border-top: 2px solid #2a70be; margin-top: -2px; background: #FFF; color: #c14545; } \r\n' +
                    '</style>');
                utils.addClass(this.menu, 'tab-menu');
                utils.addClass(this.content, 'tab-content');
            }
            for (var i = 0; i < this.length; i++) {
                this.menus[i].index = i;
                utils.removeClass(this.menus[i], this.selectedClass);
                utils.removeClass(this.contents[i], this.selectedClass);
            }
            utils.addClass(this.menus[this.index], this.selectedClass);
            utils.addClass(this.contents[this.index], this.selectedClass);
        },

        _initSize: function () {
            this.width = document.documentElement.clientWidth || document.body.clientWidth;
            this.height = document.documentElement.clientHeight || document.body.clientHeight;
            this.content.style.width = this.length * this.width + 'px';
            this.content.style.height = this.height + 'px';
            for (var i = 0; i < this.length; i++) {
                this.contents[i].style.width = this.width + 'px';
            }
            this._translate();
        },

        _resize: function () {
            var me = this;
            window.setTimeout(function () {
                me._initSize();
            }, 100);
        },

        _initEvent: function (remove) {
            var handler = remove ? utils.removeHandler : utils.addHandler;
            var me = this;

            handler(window, 'resize', function (e) {
                me._resize(e);
            });
            handler(window, 'orientationchange', function (e) {
                me._resize(e);
            });

            if (utils.hasTouch) {
                handler(this.wrap, 'touchstart', function (e) {
                    me._touchStart(e);
                });
                handler(this.wrap, 'touchmove', function (e) {
                    me._touchMove(e);
                });
                handler(this.wrap, 'touchcancel', function (e) {
                    me._touchEnd(e);
                });
                handler(this.wrap, 'touchend', function (e) {
                    me._touchEnd(e);
                });
            }

            if (utils.hasPointer) {
                handler(this.wrap, utils.prefixPointerEvent('pointerdown'), function (e) {
                    me._touchStart(e);
                });
                handler(this.wrap, utils.prefixPointerEvent('pointermove'), function (e) {
                    me._touchMove(e);
                });
                handler(this.wrap, utils.prefixPointerEvent('pointercancel'), function (e) {
                    me._touchEnd(e);
                });
                handler(this.wrap, utils.prefixPointerEvent('pointerup'), function (e) {
                    me._touchEnd(e);
                });
            } else {
                handler(this.wrap, 'mousedown', function (e) {
                    me._touchStart(e);
                });
                handler(this.wrap, 'mousemove', function (e) {
                    me._touchMove(e);
                });
                handler(this.wrap, 'mousecancel', function (e) {
                    me._touchEnd(e);
                });
                handler(this.wrap, 'mouseup', function (e) {
                    me._touchEnd(e);
                });
            }

            handler(this.menu, 'click', function (e) {
                me._touchClick(e);
            });
            handler(this.menu, 'touchend', function (e) {
                me._touchClick(e);
            });
        },

        _touchStart: function (e) {
            if (utils.eventType[e.type] !== 1) {
                var button;
                if (!e.which) {
                    button = (e.button < 2) ? 0 : ((e.button === 4) ? 1 : 2); // IE case 
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

            this.initiated = utils.eventType[e.type];
            this.touch.x = utils.event.pageX(e);
            this.touch.y = utils.event.pageY(e);
            this.touch.time = Date.now();
            this.touch.disX = 0;
            this.touch.disY = 0;
            this.touch.isHorizontalMove = false;
        },

        _touchMove: function (e) {
            if (utils.eventType[e.type] !== this.initiated) {
                return;
            }

            utils.event.stopPropagation(e);

            var point = e.touches ? e.touches[0] : e;
            if (point.length > 1 || e.scale && e.scale !== 1) {
                return;
            }

            this.touch.disX = utils.event.pageX(e) - this.touch.x;
            this.touch.disY = utils.event.pageY(e) - this.touch.y;
            this.touch.isHorizontalMove = Math.abs(this.touch.disX) > Math.abs(this.touch.disY);
            if (this.touch.isHorizontalMove) {
                utils.event.preventDefault(e);
                if ((this.index === 0 && this.touch.disX > 0) || (this.index === this.length - 1 && this.touch.disX < 0)) {
                    this.touch.disX /= 4;
                }
                this._translate(this.touch.disX - this.index * this.width);
            }
        },

        _touchEnd: function (e) {
            if (!this.initiated) {
                return;
            }

            this.initiated = false;

            if (this.touch.isHorizontalMove) {
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

                if (this.callback && typeof this.callback[this.index] === 'function') {
                    this.callback[this.index]();
                }
            }
        },

        _touchClick: function (e) {
            var target = e.target || e.srcElement;
            if (target.nodeType === 1 && target.index !== undefined) {
                if (target.index === this.index) {
                    return;
                }

                utils.event.preventDefault(e);
                utils.event.stopPropagation(e);

                this.index = target.index;
                this._translate();

                if (this.callback && typeof this.callback[this.index] === 'function') {
                    this.callback[this.index]();
                }
                this._replace();
            }
        },

        _replace: function () {
            utils.addClass(this.menus[this.index], this.selectedClass);
            utils.addClass(this.contents[this.index], this.selectedClass);
            utils.removeClass(this.menus[this.oldIndex], this.selectedClass);
            utils.removeClass(this.contents[this.oldIndex], this.selectedClass);
            this.oldIndex = this.index;
        },

        _translate: function (moveX) {
            var destX = (typeof moveX === 'number') ? moveX : -this.index * this.width;
            var duration = (typeof moveX === 'number') ? '0' : this.duration;
            if (utils.hasTransform && utils.hasTransition) {
                this.content.style[utils.prefixStyle('transition')] = 'all ' + duration + 'ms';
                this.content.style[utils.prefixStyle('transform')] = 'translate3d(' + destX + 'px,0,0)';
            } else {
                this.content.style.left = destX + 'px';
                if (moveX) {
                    this.x = moveX;
                } else {
                    this._updatePosition();
                }
            }
        },

        _updatePosition: function () {
            var me = this,
                startX = me.x || 0,
                destX = -this.index * this.width,
                duration = this.duration,
                startTime = utils.getTime(),
                destTime = startTime + duration;

            function step() {
                var now = utils.getTime(),
                    newX;

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
                    utils.rAF(step);
                }
            }

            me.isAnimating = true;
            step();
        },

        _execEvent: function (type) {
            if (!this.events[type]) {
                return;
            }

            var i = 0,
                l = this.events[type].length;

            if (!l) {
                return;
            }

            for (; i < l; i++) {
                this.events[type][i].apply(this, [].slice.call(arguments, 1));
            }
        },

        destroy: function () {
            this._initEvent(true);
        }
    };

    window.Tab = Tab;

})(window, document);