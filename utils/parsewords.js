$$(".wrap-group-list").map(
    p => $$(".word-item", p).map(k=> ({ word: $(".word",k).textContent, translate: $(".translate",k).textContent }))
)