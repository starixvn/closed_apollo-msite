Array.prototype.clear = function () {
    if (this.length > 0) this.splice(0, this.length);
}
String.prototype.isNumber = function () {
    var regs = new RegExp(/^-?\d+(\.\d+)?\s*$/);
    return (regs.test(this));
}

StringBuilder = function (initialText) {
    var _parts = new Array();
    if (initialText) _parts.append(initialText);
    this.append = function (text) {
        if (text) _parts.push(text);
    }
    this.appendLine = function (text) {
        this.append(text);
        this.append("\r\n");
    }
    this.clear = function () {
        _parts.clear();
    }
    this.isEmpty = function () {
        return (_parts.length == 0);
    }
    this.toString = function (delimiter) {
        delimiter = delimiter || '';
        return _parts.join(delimiter);
    }
}

Number.prototype.toDecimals = function (n) {
    n = (isNaN(n)) ? 2 : n;
    var num = Math.pow(10, n);
    function pad(s) {
        s = s || '.';
        return (s.length > 2) ? s : pad(s + '0');
    }
    return (isNaN(this)) ? this : (new String(Math.round(this * num) / num)).replace(/(\.\d*)?$/, pad);
}

String.prototype.startsWith = function (prefix) {
    return (this.substr(0, prefix.length) == prefix);
}

String.prototype.endsWith = function (suffix) {
    return (this.substr(this.length - suffix.length) == suffix);
}

String.prototype.contains = function (item) {
    return (this.indexOf(item) >= 0);
}

String.prototype.trimLeft = function () {
    return this.replace(/^\s*/, "");
}

String.prototype.trimRight = function () {
    return this.replace(/\s*$/, "");
}

String.prototype.trim = function () {
    return this.trimRight().trimLeft();
}

String.prototype.compare = function (args, isIgnoreCase) {
    if (isIgnoreCase) {
        return ((new String(this.toLowerCase()) == (new String(args)).toLowerCase()) ? 0 : 1);
    }
    return ((this == args) ? 0 : 1);
}

String.format = function (format) {
    for (var i = 1; i < arguments.length; i++) {
        format = format.replace("{" + (i - 1) + "}", arguments[i]);
    }
    return format;
}
function obj(id) {
    return document.getElementById(id);
}
function ErrMsg(msg) {
    obj("errmsg").style.display = '';
    obj("errmsg").innerHTML = msg;
}