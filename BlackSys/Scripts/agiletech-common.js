function Get2Date(){
    var today = new Date();

    var intDay = today.getDate();
    var strDay = "";
    if (parseInt(intDay) < 10) {
        strDay = "0" + intDay.toString();
    }

    var intMonth = today.getMonth() + 1;
    var strMonth = "";
    if (parseInt(intMonth) < 10) {
        strMonth = "0" + intMonth.toString();
    }

    return today.getFullYear().toString() + "-" + strMonth + "-" + strDay;

}
