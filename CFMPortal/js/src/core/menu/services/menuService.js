angular.module('menu').factory('MenuService', function () {
    var menuData = [];
    return {
        registerMenuData: registerMenuData,
        getMenuData: getMenuData
    }

    function registerMenuData(menu) {
        menuData.push(menu);
    }

    function getMenuData() {
        return menuData;
    }
});