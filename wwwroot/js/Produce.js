// const add = document.querySelector('#add');
// const submitBtn = document.querySelector('#submitt');
// let produce = []
// add.addEventListener('click', (e) => {
//     e.preventDefault();
//     console.log('Saving Data...')
//     const selectList = document.querySelector('#CategoryId');
//     const produceId = document.querySelector('#ProduceId');
//     const quantity = document.querySelector('#Quantity');
//     let Data = {
//         CategoryId: selectList.options[selectList.selectedIndex].value,
//         CategoryList: [selectList.options[selectList.selectedIndex].value],
//         produceId: produceId.options[produceId.selectedIndex].text,
//         QuantityToBuy: quantity.value
//     }

// console.log(produceId.options[produceId.selectedIndex].text);
// console.log(Data);
//     produce.push(Data);
// })

// submitBtn.addEventListener('click', (e) => {
//     console.log(produce)
//     e.preventDefault();
//     fetch('/Produce/Sell', {
//         method: 'POST',
//         headers: {
//             'Content-Type': 'application/json'
//         },
//         body: JSON.stringify(produce)
//     })
//         .then(response => response.json())
//         .then(data => console.log(data))
//         .catch(err => console.error(err));
// })