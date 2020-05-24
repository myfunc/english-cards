import React, { useEffect } from "react";
import { CategoryCard, Title, Wrapper } from "../../components";
import { connect } from "react-redux";
import { fetchCategories } from "../../store/actions";

function Learn(props: any) {  
    
    useEffect(() => {
        props.fetchCategories();
    }, []);
    
    return (
        <div className="learn">
            <Wrapper>
                <Title title="You can chose to learn" />
                <div className="learn__content">    
                    {props.categories.map((item: any) => <CategoryCard key={item.id} {...item}/>)}
                </div>
            </Wrapper>
        </div>
    )
}

function mapStateToProps(state: any) {
    return {
        categories: state.categories.categories,
        loading: state.categories.loading
    }
}

function mapDispatchToProps(dispatch: any) {
    return {
        fetchCategories: () => dispatch(fetchCategories())
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Learn);