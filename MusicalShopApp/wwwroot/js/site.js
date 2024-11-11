//import "jquery.maskedinput.min.js"
//import "jquery.min.js"
//$('.mask-phone').mask('+7 (999) 999-99-99');

//$(document).ready(function () {
//    $('table.all-users tr').click(function () {
//        window.location = $(this).attr('href');
//        return false;
//    });
//});

$(document).ready(function () {
    $('form').attr('autocomplete', 'off');
});
// okey, i did my best. this stuff doesn't work and i don't know why. i should improve my js skills to understand this thing, but i don't want to teach js. what should i do, hmm. 7 seconds after writing down these comments: GOT IT. it works. inappropriate way. now i realize why ids are mandatory here. hehe. fun fact: it'll be in my diploma?? i don't want to rub off all my comments with thoughts, so looks like it'll.
var checkLists = document.getElementsByClassName('dropdown-check-list');
for (checkList of checkLists) {
    checkList.getElementsByClassName('anchor')[0].onclick = function (evt) {
        //alert('works');
        if (checkList.classList.contains('visible')) {
            checkList.classList.remove('visible');
        }
        else {
            checkList.classList.add('visible');
        }
    }
}

$(document).ready(function () {
    $('.add-remove-cart-button').on('click', function (e) {
        button = e.target;
        markSwitched(button);
        form = button.closest('form');
        input = form.children[2];
        input.dataset.value = input.dataset.value == 'true' ? 'false' : 'true';
        //form.submit();
    });
});

function markSwitched(button) {
    button.innerHTML = button.innerHTML == '+' ? '-' : '+';
}

$(document).ready(function () {
    let radioButtons = document.getElementsByClassName('new-goods-kind-radio');
    for (let i = 0; i < radioButtons.length; i++) {
        radioButtons[i].addEventListener('change', function () {
            let specificDataDiv = document.getElementById('specific-data');
            removeChildren(specificDataDiv);
            if (this.value == 'MusicalInstrument') {
                // release year
                let releaseYearId = 'new-goods-release-year';
                let releaseYearDiv = document.createElement('div');

                let releaseYearLabel = document.createElement('label');
                releaseYearLabel.setAttribute('for', releaseYearId);
                releaseYearLabel.innerHTML = 'Год выпуска';
                releaseYearDiv.appendChild(releaseYearLabel);

                let releaseYear = document.createElement('input');
                releaseYear.setAttribute('id', releaseYearId);
                releaseYear.setAttribute('name', 'AddGoodsToWarehouseDto.GoodsKindSpecificDataDto.ReleaseYear');
                releaseYear.setAttribute('type', 'number');
                releaseYearDiv.appendChild(releaseYear);
                specificDataDiv.appendChild(releaseYearDiv);
                // manufacturer type
                let manufacturerTypeId = 'new-goods-manufacturer-type';

                let manufacturerTypeLabel = document.createElement('label');
                manufacturerTypeLabel.setAttribute('for', manufacturerTypeId);
                manufacturerTypeLabel.innerHTML = 'Тип производителя';
                specificDataDiv.appendChild(manufacturerTypeLabel);

                let manufacturerTypeRadioGroup = document.createElement('span');
                manufacturerTypeRadioGroup.setAttribute('id', manufacturerTypeId);

                let manufacturerTypeFactoryId = manufacturerTypeId + '-factory';
                let manufacturerTypeFactory = document.createElement('input');
                manufacturerTypeFactory.setAttribute('id', manufacturerTypeFactoryId);
                manufacturerTypeFactory.setAttribute('name', 'AddGoodsToWarehouseDto.GoodsKindSpecificDataDto.ManufacturerType');
                manufacturerTypeFactory.setAttribute('value', 'Factory');
                manufacturerTypeFactory.setAttribute('type', 'radio');
                manufacturerTypeRadioGroup.appendChild(manufacturerTypeFactory);

                let manufacturerTypeFactoryLabel = document.createElement('label');
                manufacturerTypeFactoryLabel.setAttribute('for', manufacturerTypeFactoryId);
                manufacturerTypeFactoryLabel.innerHTML = 'Завод';
                manufacturerTypeRadioGroup.appendChild(manufacturerTypeFactoryLabel);

                let manufacturerTypeMasterId = manufacturerTypeId + '-Master';
                let manufacturerTypeMaster = document.createElement('input');
                manufacturerTypeMaster.setAttribute('id', manufacturerTypeMasterId);
                manufacturerTypeMaster.setAttribute('name', 'AddGoodsToWarehouseDto.GoodsKindSpecificDataDto.ManufacturerType');
                manufacturerTypeMaster.setAttribute('value', 'Master');
                manufacturerTypeMaster.setAttribute('type', 'radio');
                manufacturerTypeRadioGroup.appendChild(manufacturerTypeMaster);

                let manufacturerTypeMasterLabel = document.createElement('label');
                manufacturerTypeMasterLabel.setAttribute('for', manufacturerTypeMasterId);
                manufacturerTypeMasterLabel.innerHTML = 'Мастер';
                manufacturerTypeRadioGroup.appendChild(manufacturerTypeMasterLabel);

                specificDataDiv.appendChild(manufacturerTypeRadioGroup);
                // manufacturer name
                let manufacturerNameDiv = document.createElement('div');
                let manufacturerNameLabel = document.createElement('label');
                let manufacturerNameId = 'new-goods-manufacturer-name';
                manufacturerNameLabel.setAttribute('for', manufacturerNameId);
                manufacturerNameLabel.innerHTML = 'Название производителя';
                manufacturerNameDiv.appendChild(manufacturerNameLabel);

                let manufacturerNameInput = document.createElement('input');
                manufacturerNameInput.setAttribute('id', manufacturerNameId);
                manufacturerNameInput.setAttribute('type', 'text');
                manufacturerNameInput.setAttribute('name', 'AddGoodsToWarehouseDto.GoodsKindSpecificDataDto.Manufacturer');
                manufacturerNameDiv.appendChild(manufacturerNameInput);

                specificDataDiv.appendChild(manufacturerNameDiv);
            }
            else if (this.value == 'Accessory') {
                removeChildren(specificDataDiv);

            }
            else if (this.value == 'AudioEquipmentUnit') {
                removeChildren(specificDataDiv);

            }
            else if (this.value == 'SheetMusicEdition') {
                removeChildren(specificDataDiv);

            }
        });
    };
});

function removeChildren(element) {
    element.innerHTML = '';
}